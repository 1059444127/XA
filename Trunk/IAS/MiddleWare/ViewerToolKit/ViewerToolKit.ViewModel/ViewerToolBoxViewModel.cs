using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Xml.Linq;
using UIH.XA.Common.MVVM;
using UIH.XA.ViewerToolKit.Config;
using UIH.XA.ViewerToolKit.Interface;
using UIH.XA.ViewerToolKit.Utility;

namespace UIH.XA.ViewerToolKit.ViewModel
{
    public abstract class ViewerToolBoxViewModel : NotificationObject
    {
        [Import("ViewerToolBoxConfig", typeof (string))] private string _configPath;
        [Import] protected IViewerToolFactory _model;
        protected string _name;
        private ObservableCollection<ToolViewModel> _tools;

        public ObservableCollection<ToolViewModel> Tools
        {
            get { return _tools ?? (_tools = new ObservableCollection<ToolViewModel>(CreateTools())); }
        }

        private IEnumerable<ToolViewModel> CreateTools()
        {
            var xDoc = XDocument.Load(_configPath);
            var xViewerToolBox =
                xDoc.Descendants()
                    .FirstOrDefault(
                        e =>
                            e.Name.LocalName == ViewerToolBoxName.ViewerToolBox &&
                            e.GetAttribute(ViewerToolBoxName.NameAttribute) == _name);


            var tools = new List<ToolViewModel>();
            if (xViewerToolBox == null) return tools;
            var isViewerToolBoxAvailable = xViewerToolBox.GetBoolAttribute(ViewerToolBoxName.Available);
            if (!isViewerToolBoxAvailable) return tools;


            foreach (var xTool in xViewerToolBox.Descendants())
            {
                var isToolAvailable = xTool.GetBoolAttribute(ViewerToolBoxName.Available);
                if (!isToolAvailable) continue;

                var group = xTool.GetAttribute(ViewerToolBoxName.Group);
                if (string.IsNullOrWhiteSpace(group))
                {
                    tools.Add(new ButtonToolViewModel(_model.CreateTool(xTool)));
                }
                else
                {
                    var toolGroup = tools.OfType<ToolGroupViewModel>().FirstOrDefault(t => t.Name == group);
                    if (toolGroup == null)
                    {
                        toolGroup = new ToolGroupViewModel(group);
                        tools.Add(toolGroup);
                    }
                    toolGroup.Tools.Add(new ToolGroupItemViewModel(_model.CreateTool(xTool)));
                }
            }


            return tools;
        }
    }
}
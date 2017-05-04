using System.Collections.Generic;
using System.Xml.Linq;
using UIH.XA.ViewerToolKit.Interface;

namespace UIH.XA.ViewerToolKit.ViewerTool
{
    public class ToolBoxModelBase : IToolBoxModel
    {
        #region Implementation of IToolBoxModel

        public void Register(IViewDisplay viewDisplay)
        {
            _viewDisplays.Add(viewDisplay);
        }

        public IViewerTool CreateTool(XElement xTool)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private readonly IList<IViewDisplay> _viewDisplays = new List<IViewDisplay>();
    }
}
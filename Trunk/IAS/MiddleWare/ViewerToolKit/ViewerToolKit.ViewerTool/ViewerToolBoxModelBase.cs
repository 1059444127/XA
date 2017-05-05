using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using UIH.XA.ViewerToolKit.Interface;

namespace UIH.XA.ViewerToolKit.ViewerTool
{
    public abstract class ViewerToolBoxModelBase : IViewerToolBoxModel
    {
        public ViewerToolBoxModelBase()
        {
            
        }

        #region Implementation of IViewerToolBoxModel

        public void Register(IViewDisplay viewDisplay)
        {
            _viewDisplays.Add(viewDisplay);
        }

        public IViewerTool CreateTool(XElement xTool)
        {

            //var tool = Assembly.GetCallingAssembly().CreateInstance()
            //var toolName = xTool.Name.LocalName;
            //switch (toolName)
            //{
            //    case "LLabel" : return 
            //} 
            return null;
        }

        #endregion

        #region [--Template Tool Creator--]

        private string _assemblyName;

        protected virtual IViewerTool CreateLLabel(Dictionary<string, string> paramDictionary)
        {
            return null;
        }
        #endregion

        private readonly IList<IViewDisplay> _viewDisplays = new List<IViewDisplay>();
        private readonly Dictionary<string, Func<Dictionary<string, string>, IViewerTool>> _toolCreatorDictionary;
    }
}
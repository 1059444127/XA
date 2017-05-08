using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using UIH.XA.ViewerToolKit.Interface;
using UIH.XA.ViewerToolKit.Utility;

namespace UIH.XA.ViewerToolKit.ViewerTool
{
    public abstract class ViewerToolFactoryBase : IViewerToolFactory
    {
        protected ViewerToolFactoryBase()
        {
            _namespace = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            var ss = new StackTrace(true);
            var mb = ss.GetFrame(1).GetMethod();
            _appNamespace = mb.DeclaringType.Namespace;
        }

        #region Implementation of IViewerToolFactory

        public void Register(IViewDisplay viewDisplay)
        {
            _viewDisplays.Add(viewDisplay);
        }

        public IViewerTool CreateTool(XElement xTool)
        {
            var toolName = xTool.Name.LocalName;
            var appToolTypeName = string.Format("{0}.{1}{2}", _appNamespace, GetAppName(), toolName);
            var toolTypeName = string.Format("{0}.{1}", _namespace, toolName);

            var tool = (ViewerTool) Assembly.GetAssembly(GetType()).CreateInstance(appToolTypeName)
                       ?? (ViewerTool) Assembly.GetExecutingAssembly().CreateInstance(toolTypeName);

            if(tool == null) return new NullTool();

            //TODO: Set Parameter
            tool.Name = toolName;

            var properties = tool.GetType().GetProperties();
            foreach (var xAttribute in xTool.Attributes())
            {
                var property = properties.FirstOrDefault(f => f.Name == xAttribute.Name.LocalName);
                if(property == null) continue;

                property.SetValue(tool, xAttribute.Value);
            }

            return tool;
        }

        #endregion

        #region [--Template Tool Creator--]

        private readonly string _namespace;
        private readonly string _appNamespace;
        protected abstract string GetAppName();

        #endregion

        private readonly IList<IViewDisplay> _viewDisplays = new List<IViewDisplay>();
    }
}
using System.ComponentModel.Composition;
using UIH.XA.ViewerToolKit.Interface;
using UIH.XA.ViewerToolKit.ViewerTool;

namespace UIH.XA.ViewerToolKit.AppViewerToolFactory
{
    [Export(typeof(IViewerToolFactory))]
    public class AppViewerToolFactory : ViewerToolFactoryBase
    {
        #region Overrides of ViewerToolFactoryBase

        protected override string GetAppName()
        {
            return Name;
        }

        #endregion

        private const string Name = "App";
    }
}
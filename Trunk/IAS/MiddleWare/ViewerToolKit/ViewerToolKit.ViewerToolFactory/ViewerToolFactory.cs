using System.ComponentModel.Composition;
using UIH.XA.ViewerToolKit.Interface;
using UIH.XA.ViewerToolKit.ViewerTool;

namespace UIH.XA.ViewerToolKit.ViewerToolFactory
{
    [Export(typeof(IViewerToolFactory))]
    public class ViewerToolFactory : ViewerToolFactoryBase
    {

        #region Overrides of ViewerToolFactoryBase

        protected override string GetAppName()
        {
            return string.Empty;
        }

        #endregion
    }
}
using System.ComponentModel.Composition;
using UIH.XA.ViewerToolKit.Interface;
using UIH.XA.ViewerToolKit.ViewerTool;

namespace UIH.XA.ViewerToolKit.ViewerToolModel
{
    [Export(typeof(IViewerToolBoxModel))]
    public class ViewerToolBoxModel : ViewerToolBoxModelBase
    {
    }
}
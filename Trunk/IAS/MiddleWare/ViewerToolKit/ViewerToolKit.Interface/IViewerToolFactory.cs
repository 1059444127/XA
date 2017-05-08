using System.Xml.Linq;

namespace UIH.XA.ViewerToolKit.Interface
{
    public interface IViewerToolFactory
    {
        void Register(IViewDisplay viewDisplay);
        IViewerTool CreateTool(XElement xTool);
    }
}
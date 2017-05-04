using System.Xml.Linq;

namespace UIH.XA.ViewerToolKit.Interface
{
    public interface IToolBoxModel
    {
        void Register(IViewDisplay viewDisplay);
        IViewerTool CreateTool(XElement xTool);
    }
}
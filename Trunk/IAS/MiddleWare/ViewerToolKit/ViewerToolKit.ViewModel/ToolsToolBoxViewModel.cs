using System.ComponentModel.Composition;
using UIH.XA.ViewerToolKit.Config;

namespace UIH.XA.ViewerToolKit.ViewModel
{
    [Export]
    public class ToolsToolBoxViewModel : ViewerToolBoxViewModel
    {
        public ToolsToolBoxViewModel()
        {
            _name = ViewerToolBoxName.Tools;
        }
    }
}
using System.ComponentModel.Composition;
using UIH.XA.ViewerToolKit.Config;

namespace UIH.XA.ViewerToolKit.ViewModel
{
    [Export]
    public class LayoutToolBoxViewModel : ViewerToolBoxViewModel
    {
        public LayoutToolBoxViewModel()
        {
            _name = ViewerToolBoxName.Layout;
        }

    }
}
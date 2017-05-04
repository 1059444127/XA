using UIH.XA.Common.MVVM;

namespace UIH.XA.ViewerToolKit.ViewModel
{
    public abstract class ToolViewModel : NotificationObject
    {
        public abstract string Name { get; }
    }
}
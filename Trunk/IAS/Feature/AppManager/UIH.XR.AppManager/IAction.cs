
namespace UIH.XR.AppManager
{
    public interface IAction
    {
        bool CanExecute(object arg);

        void Execute(object arg);
    }
}

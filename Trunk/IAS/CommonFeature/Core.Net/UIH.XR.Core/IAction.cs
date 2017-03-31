
namespace UIH.XR.Core
{
    public interface IAction
    {
        bool CanExecute();

        void Execute(object[] obj);
    }
}

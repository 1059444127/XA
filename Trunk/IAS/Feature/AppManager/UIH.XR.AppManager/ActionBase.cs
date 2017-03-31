
namespace UIH.XR.AppManager
{
    public abstract class ActionBase : IAction
    {
        public abstract bool CanExecute(object arg);

        public abstract void Execute(object arg);

        public XShellProxy xShellProxy;

        public ActionBase(string shellName,string receiver)
        {
            xShellProxy = new XShellProxy(shellName, receiver);
        }
    }
}

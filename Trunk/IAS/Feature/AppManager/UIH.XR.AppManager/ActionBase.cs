using System;
namespace UIH.XR.AppManager
{
    public abstract class ActionBase : IAction
    {
        public abstract bool CanExecute(object arg);

        public abstract void Execute(object arg);

        public XShellManager xshellManager { get; set; }

        public XShellProxy xShellProxy;

        public ActionBase(string shellName,string receiver)
        {
            Console.WriteLine("ActionBase begin construct");

            if (null == xshellManager)
            {
                Console.WriteLine("ActionBase xshellManager is null");
            }
            xShellProxy = new XShellProxy(xshellManager._remoteInvoker,shellName,receiver);
        }
    }
}

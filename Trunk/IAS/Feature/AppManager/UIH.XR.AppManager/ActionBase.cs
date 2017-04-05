using System;

namespace UIH.XR.AppManager
{
    public abstract class ActionBase : IAction
    {
        public abstract bool CanExecute(object arg);

        public abstract void Execute(object arg);

        public XShellManager xshellManager { get; set; }

        public ActionBase()
        {
            Console.WriteLine("ActionBase begin construct");
            xshellManager = XShellManager.GetInstance();
            if (null == xshellManager)
            {
                Console.WriteLine("ActionBase xshellManager is null");
            }
        }
    }
}

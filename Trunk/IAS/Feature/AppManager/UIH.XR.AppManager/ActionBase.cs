using System;
using UIH.XR.StateMachine;

namespace UIH.XR.AppManager
{
    public abstract class ActionBase : IAction
    {
        public abstract bool CanExecute(object arg);

        public abstract void Execute(object arg);

        public XShellManager xshellManager { get; private set; }

        /// <summary>
        /// construct
        /// </summary>
        public ActionBase()
        {
            Console.WriteLine("ActionBase begin construct");
            xshellManager = XShellManager.GetInstance();
        }
    }
}

﻿using UIH.XA.StateMachine;

namespace UIH.XA.AppManager
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
            xshellManager = XShellManager.GetInstance();
        }
    }
}

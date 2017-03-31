
using System;

namespace UIH.XR.AppManager.Actions
{
    public class EnterServiceAction : ActionBase
    {
        public EnterServiceAction()
            : base("shellName", "receiver")
        {
        }

        public override bool CanExecute(object arg)
        {
            Console.WriteLine("EnterServiceAction CanExecute,shellName is:" + this.xShellProxy.ShellName);
            return true;
        }

        public override void Execute(object arg)
        {
            bool executeResult=this.xShellProxy.HideShell();
            Console.WriteLine("EnterServiceAction result:" + executeResult);
        }
    }
}

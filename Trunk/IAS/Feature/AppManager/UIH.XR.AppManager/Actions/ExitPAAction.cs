
using System;

namespace UIH.XR.AppManager.Actions
{
    public class ExitPAAction : ActionBase
    {
        public ExitPAAction()
            : base("shellName", "receiver")
        {
        }

        public override bool CanExecute(object arg)
        {
            Console.WriteLine("ExitPAAction CanExecute,shellName is:" + this.xShellProxy.ShellName);
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("ExitPAAction ok");
        }
    }
}

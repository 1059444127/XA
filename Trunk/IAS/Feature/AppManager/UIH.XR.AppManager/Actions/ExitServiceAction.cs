
using System;

namespace UIH.XR.AppManager.Actions
{
    public class ExitServiceAction : ActionBase
    {
        public ExitServiceAction()
            : base("shellName", "receiver")
        {
        }

        public override bool CanExecute(object arg)
        {
            Console.WriteLine("ExitServiceAction CanExecute,shellName is:" + this.xShellProxy.ShellName);
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("ExitServiceAction ok");
        }
    }
}


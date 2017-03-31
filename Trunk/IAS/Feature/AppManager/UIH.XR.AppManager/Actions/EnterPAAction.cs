
using System;

namespace UIH.XR.AppManager.Actions
{
    public class EnterPAAction : ActionBase
    {
        public EnterPAAction()
            : base("shellName", "receiver")
        {
        }

        public override bool CanExecute(object arg)
        {
            Console.WriteLine("EnterPAAction CanExecute,shellName is:" + this.xShellProxy.ShellName);
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("EnterPAAction ok");
        }
    }
}

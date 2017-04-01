

using System;

namespace UIH.XR.AppManager.Actions
{
    public class EnterIdleAction : ActionBase
    {
        public EnterIdleAction()
            : base("shellName", "receiver")
        {
            Console.WriteLine("EnterIdleAction 构造 ");
        }

        public override bool CanExecute(object arg)
        {
            Console.WriteLine("EnterIdleAction CanExecute,shellName is:" + this.xShellProxy.ShellName);
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("EnterIdleAction ok");
        }
    }
}

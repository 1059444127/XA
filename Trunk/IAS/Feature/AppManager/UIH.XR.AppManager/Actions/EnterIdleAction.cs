

using System;

namespace UIH.XR.AppManager.Actions
{
    public class EnterIdleAction : ActionBase
    {
        public override bool CanExecute(object arg)
        {
            Console.WriteLine("EnterIdleAction CanExecute,shellName is:" + this.xshellManager.GetShell("shellName"));
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("EnterIdleAction ok");
        }
    }
}

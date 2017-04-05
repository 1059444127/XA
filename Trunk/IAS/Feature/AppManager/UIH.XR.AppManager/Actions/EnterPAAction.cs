
using System;

namespace UIH.XR.AppManager.Actions
{
    public class EnterPAAction : ActionBase
    {
        public override bool CanExecute(object arg)
        {
            Console.WriteLine("EnterPAAction CanExecute,shellName is:" + this.xshellManager.GetShell("shellName"));
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("EnterPAAction ok");
        }
    }
}

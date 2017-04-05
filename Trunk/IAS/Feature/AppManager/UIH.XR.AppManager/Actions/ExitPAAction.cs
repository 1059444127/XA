
using System;

namespace UIH.XR.AppManager.Actions
{
    public class ExitPAAction : ActionBase
    {
        public override bool CanExecute(object arg)
        {
            Console.WriteLine("ExitPAAction CanExecute,shellName is:" + this.xshellManager.GetShell("shellName"));
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("ExitPAAction ok");
        }
    }
}

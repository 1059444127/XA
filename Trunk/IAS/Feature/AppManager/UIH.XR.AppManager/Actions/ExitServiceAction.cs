
using System;

namespace UIH.XR.AppManager.Actions
{
    public class ExitServiceAction : ActionBase
    {
       
        public override bool CanExecute(object arg)
        {
            Console.WriteLine("ExitServiceAction CanExecute,shellName is:" + this.xshellManager.GetShell("shellName"));
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("ExitServiceAction ok");
        }
    }
}



using System;

namespace UIH.XR.AppManager.Actions
{
    public class SuspendStudyAction : ActionBase
    {
        public override bool CanExecute(object arg)
        {
            Console.WriteLine("SuspendStudyAction CanExecute,shellName is:" + this.xshellManager.GetShell("shellName"));
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("SuspendStudyAction ok");
        }
    }
}

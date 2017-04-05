

using System;

namespace UIH.XR.AppManager.Actions
{
    public class LoadStudyAction : ActionBase
    {

        public override bool CanExecute(object arg)
        {
            Console.WriteLine("LoadStudyAction CanExecute,shellName is:" + this.xshellManager.GetShell("shellName"));
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("LoadStudyAction ok");
        }
    }
}

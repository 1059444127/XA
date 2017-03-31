

using System;

namespace UIH.XR.AppManager.Actions
{
    public class LoadStudyAction : ActionBase
    {
        public LoadStudyAction()
            : base("shellName", "receiver")
        {
        }

        public override bool CanExecute(object arg)
        {
            Console.WriteLine("LoadStudyAction CanExecute,shellName is:" + this.xShellProxy.ShellName);
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("LoadStudyAction ok");
        }
    }
}

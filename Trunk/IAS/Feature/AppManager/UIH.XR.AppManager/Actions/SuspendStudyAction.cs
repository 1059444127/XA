
using System;

namespace UIH.XR.AppManager.Actions
{
    public class SuspendStudyAction : ActionBase
    {
        public SuspendStudyAction()
            : base("shellName", "receiver")
        {
        }

        public override bool CanExecute(object arg)
        {
            Console.WriteLine("SuspendStudyAction CanExecute,shellName is:" + this.xShellProxy.ShellName);
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("SuspendStudyAction ok");
        }
    }
}

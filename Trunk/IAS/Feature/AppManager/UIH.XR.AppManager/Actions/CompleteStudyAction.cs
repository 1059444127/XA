
using System;

namespace UIH.XR.AppManager.Actions
{
    public class CompleteStudyAction : ActionBase
    {
        public CompleteStudyAction()
            : base("shellName", "receiver")
        {
        }

        public override bool CanExecute(object arg)
        {
            Console.WriteLine("CompareImageAction CanExecute,shellName is:" + this.xShellProxy.ShellName);
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("CompleteStudyAction ok");
        }
    }
}

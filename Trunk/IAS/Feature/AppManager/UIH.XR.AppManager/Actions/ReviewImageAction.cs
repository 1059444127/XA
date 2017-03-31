
using System;

namespace UIH.XR.AppManager.Actions
{
    public class ReviewImageAction : ActionBase
    {
        public ReviewImageAction()
            : base("shellName", "receiver")
        {
        }

        public override bool CanExecute(object arg)
        {
            Console.WriteLine("ReviewImageAction CanExecute,shellName is:" + this.xShellProxy.ShellName);
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("ReviewImageAction ok");
        }
    }
}

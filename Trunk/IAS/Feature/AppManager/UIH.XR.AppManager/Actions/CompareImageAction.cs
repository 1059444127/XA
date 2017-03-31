using System;

namespace UIH.XR.AppManager.Actions
{
    public class CompareImageAction : ActionBase
    {
        public CompareImageAction()
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
            Console.WriteLine("CompareImageAction ok");
        }
    }
}


using System;

namespace UIH.XR.AppManager.Actions
{
    public class ReviewImageAction : ActionBase
    {
        public override bool CanExecute(object arg)
        {
            Console.WriteLine("ReviewImageAction CanExecute,shellName is:" + this.xshellManager.GetShell("shellName"));
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("ReviewImageAction ok");
        }
    }
}

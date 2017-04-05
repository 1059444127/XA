
using System;

namespace UIH.XR.AppManager.Actions
{
    public class EnterServiceAction : ActionBase
    {
        public override bool CanExecute(object arg)
        {
            Console.WriteLine("EnterServiceAction CanExecute,shellName is:" + this.xshellManager.GetShell("shellName"));
            return true;
        }

        public override void Execute(object arg)
        {
            Console.WriteLine("EnterServiceAction result:" );
        }
    }
}

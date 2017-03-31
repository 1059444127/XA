using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XR.StateMachine;

namespace UIH.XR.StateMachineUT.TestActions
{
    public class MyAction:IAction
    {
        public bool CanExecute(object argv)
        {
            return true;
        }

        public void Execute(object argv)
        {
            Console.WriteLine("MyAction::Execute()");
        }
    }
}

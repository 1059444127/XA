using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XR.StateMachine;

namespace UIH.XR.StateMachineUT.TestActions
{
    public class EnterInitialAction:IAction
    {
        public bool CanExecute(object argv)
        {
            Console.WriteLine("EnterInitialAction::CanExecute");
            return true;
        }

        public void Execute(object argv)
        {
            Console.WriteLine("EnterInitialAction::Execute");
        }
    }
}

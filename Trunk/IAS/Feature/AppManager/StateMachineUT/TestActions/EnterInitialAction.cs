using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XA.StateMachine;

namespace UIH.XA.StateMachineUT.TestActions
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

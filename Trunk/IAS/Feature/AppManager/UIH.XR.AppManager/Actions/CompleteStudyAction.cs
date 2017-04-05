
using System;

namespace UIH.XR.AppManager.Actions
{
    public class CompleteStudyAction : ActionBase
    {
        public CompleteStudyAction():base()
        {
            Console.WriteLine("CompleteStudyAction construct.");
        }

        public override bool CanExecute(object arg)
        {
            try
            {
                Console.WriteLine("CompleteStudyAction CanExecute,shellName is:" + this.xshellManager.GetShell("shellName"));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CompleteStudyAction CanExecute ex:" +ex.Message);
                return false;
            }
          
        }

        public override void Execute(object arg)
        {
            try
            {
                bool resut = this.xshellManager.GetShell("").ShowShell();
                Console.WriteLine("CompleteStudyAction resut:" + resut);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CompleteStudyAction Execute ex:" + ex.Message);
            }
        }
    }
}

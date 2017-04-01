using UIH.XR.Core;
using UIH.XR.StateMachine.Default;
using System.Reflection;
using System.IO;
using UIH.XR.StateMachine;
using UIH.Mcsf.Log;

namespace UIH.XR.AppManager
{
    public class AppManager : IWorkflow
    {
        public string CurrentProcedure { get; set; }
        
        public bool Invoke(string transitionID, object context)
        {
            CLRLogger.GetInstance().LogDevInfo(string.Format("AppManager begin Invoke,transitionID:{0}", transitionID));
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream fs = asm.GetManifestResourceStream("UIH.XR.AppManager.SimpleStateMachine.xml");
            IStateMachine sm = new DefaultStateMachineFactory().CreateStateMachineFromXml(fs);
            sm.Transit(transitionID, context);
            return true;
        }
    }
}

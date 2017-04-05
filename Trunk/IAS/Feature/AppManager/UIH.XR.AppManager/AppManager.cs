using UIH.XR.Core;
using UIH.XR.StateMachine.Default;
using System.Reflection;
using System.IO;
using UIH.XR.StateMachine;
using UIH.Mcsf.Log;
using UIH.Mcsf.Core;
using System;

namespace UIH.XR.AppManager
{
    public class AppManager : IWorkflow
    {
        public string CurrentProcedure { get; set; }

        public XShellManager _xshellManager;

        private ICommunicationProxy _communicationProxy;

        public AppManager(ICommunicationProxy communicationProxy)
        {
            Console.WriteLine("AppManager construct");
            _communicationProxy = communicationProxy;
        }

        public void Initialize()
        {
            Console.WriteLine("AppManager Initialize");

            _xshellManager = XShellManager.GetInstance();

            _xshellManager.Initialize(_communicationProxy);

            _xshellManager._remoteInvoker.RegisterServiceObject<IWorkflow>(this);

            Console.WriteLine("AppManager Initialize end");  
        }

        public bool Invoke(string transitionID, object context)
        {
            CLRLogger.GetInstance().LogDevInfo(string.Format("AppManager begin Invoke,transitionID:{0}", transitionID));

            Console.WriteLine("AppManager Invoke begin");  

            Assembly asm = Assembly.GetExecutingAssembly();

            Stream fs = asm.GetManifestResourceStream("UIH.XR.AppManager.SimpleStateMachine.xml");

            IStateMachine sm = new DefaultStateMachineFactory().CreateStateMachineFromXml(fs);

            sm.Transit(transitionID, context);

            return true;
        }
    }
}

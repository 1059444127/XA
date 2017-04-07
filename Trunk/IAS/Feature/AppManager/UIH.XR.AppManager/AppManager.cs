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
        private static string stateMachineConfigXml = mcsf_clr_systemenvironment_config.GetApplicationPath() + @"xsample\config\SimpleStateMachine.xml";

        public string CurrentProcedure { get; set; }

        public XShellManager _xshellManager;

        public ICommunicationProxy _communicationProxy { get; private set; }

        private IStateMachine _stateMachine;

        public AppManager(ICommunicationProxy communicationProxy)
        {
            Console.WriteLine("AppManager construct");
            _communicationProxy = communicationProxy;
        }

        public void Initialize()
        {
            Console.WriteLine("AppManager Initialize");

            _stateMachine = new DefaultStateMachineFactory().CreateStateMachineFromXml(stateMachineConfigXml);

            _xshellManager = XShellManager.GetInstance();

            _xshellManager.Initialize(_communicationProxy);

            _xshellManager._remoteInvoker.RegisterServiceObject<IWorkflow>(this);

            Console.WriteLine("AppManager Initialize end");
        }

        public bool Invoke(string transitionID, object context)
        {
            CLRLogger.GetInstance().LogDevInfo(string.Format("AppManager begin Invoke,transitionID:{0}", transitionID));

            Console.WriteLine("AppManager Invoke begin");

            _stateMachine.Transit(transitionID, context);

            return true;
        }
    }
}

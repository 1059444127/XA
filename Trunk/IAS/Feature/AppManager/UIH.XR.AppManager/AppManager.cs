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

        private DefaultStateMachine _stateMachine;

        /// <summary>
        /// construct
        /// </summary>
        /// <param name="communicationProxy">proxy object</param>
        public AppManager(ICommunicationProxy communicationProxy)
        {
            Console.WriteLine("AppManager construct");
            _communicationProxy = communicationProxy;
        }

        /// <summary>
        /// Initialize,instantiate xshellManager and stateMachine 
        /// </summary>
        public void Initialize()
        {
            Console.WriteLine("AppManager Initialize");

            IStateMachine stateMachine = new DefaultStateMachineFactory().CreateStateMachineFromXml(stateMachineConfigXml);

            _stateMachine = stateMachine as DefaultStateMachine;

            _xshellManager = XShellManager.GetInstance();

            _xshellManager.Initialize(_communicationProxy);

            _xshellManager._remoteInvoker.RegisterServiceObject<IWorkflow>(this);

            Console.WriteLine("AppManager Initialize end");
        }

        /// <summary>
        /// execute statemachine's Transit function, this function is registered for RMI
        /// </summary>
        /// <param name="transitionID">transition id</param>
        /// <param name="context">app's context</param>
        /// <returns>true:success
        ///          false:fail
        /// </returns>
        public bool Invoke(string transitionID, object context)
        {
            CLRLogger.GetInstance().LogDevInfo(string.Format("AppManager begin Invoke,transitionID:{0}", transitionID));

            Console.WriteLine(string.Format("AppManager begin Invoke,transitionID:{0},current state id:{1}", transitionID, _stateMachine.CurrentState.ID));

            _stateMachine.Transit(transitionID, context);

            return true;
        }
    }
}

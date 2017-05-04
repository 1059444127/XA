using UIH.XA.Core;
using UIH.XA.StateMachine.Default;
using UIH.XA.StateMachine;
using UIH.Mcsf.Log;
using UIH.Mcsf.Core;

namespace UIH.XA.AppManager
{
    public class AppManager : IWorkflow
    {
        private static string stateMachineConfigXml = mcsf_clr_systemenvironment_config.GetApplicationPath() + @"xsample\config\SimpleStateMachine.xml";
        public string CurrentProcedure { get; set; }
        private DefaultStateMachine _stateMachine;
        private XShellManager _xshellManager; 

        /// <summary>
        /// Initialize,instantiate xshellManager and stateMachine 
        /// </summary>
        public void Initialize(IRemoteMethodInvoker remoteInvoker)
        {
            IStateMachine stateMachine = new DefaultStateMachineFactory().CreateStateMachineFromXml(stateMachineConfigXml);
            _stateMachine = stateMachine as DefaultStateMachine;
            _xshellManager = XShellManager.GetInstance();
            _xshellManager.Initialize(remoteInvoker);
            _xshellManager._remoteInvoker.RegisterServiceObject<IWorkflow>(this);
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
            _stateMachine.Transit(transitionID, context);
            return true;
        }
    }
}

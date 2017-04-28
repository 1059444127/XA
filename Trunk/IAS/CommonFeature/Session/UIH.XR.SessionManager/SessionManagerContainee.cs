using UIH.Mcsf.Core;
using UIH.XR.GlobalParameter;

namespace UIH.XR.SessionManager
{
    public class SessionManagerContainee : CLRContaineeBase
    {
        public ICommunicationProxy _iCommunicationProxy;

        private ProcessCacheCommandHandler _handleProcessStatusCommand;
        private SystemCacheCommandHandler _handleSystemDataCommand;

        public override void DoWork()
        {
            _iCommunicationProxy.RegisterCommandHandler(CommunicationCommandID.IAS_SESSION_CACHE_PROCESS_STATUS, _handleProcessStatusCommand);
            _iCommunicationProxy.RegisterCommandHandler(CommunicationCommandID.IAS_SESSION_CACHE_SYSTEM_DATA, _handleSystemDataCommand);
        }

        public override void Startup()
        {
            base.Startup();
            _iCommunicationProxy = GetCommunicationProxy();
            _handleProcessStatusCommand = new ProcessCacheCommandHandler();
            _handleSystemDataCommand = new SystemCacheCommandHandler();
        }
    }
}

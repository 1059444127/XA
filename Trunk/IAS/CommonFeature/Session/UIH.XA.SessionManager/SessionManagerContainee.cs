using UIH.Mcsf.Core;
using UIH.XA.GlobalParameter;

namespace UIH.XA.SessionManager
{
    public class SessionManagerContainee : CLRContaineeBase
    {
        public ICommunicationProxy _iCommunicationProxy;

        private ProcessCacheCommandHandler _handleProcessStatusCommand;
        private SystemCacheCommandHandler _handleSystemDataCommand;

        public override void DoWork()
        {
            _iCommunicationProxy.RegisterCommandHandler(CommunicationCommandID.XA_SYSTEMSESSION_PROCESS_CACHE, _handleProcessStatusCommand);
            _iCommunicationProxy.RegisterCommandHandler(CommunicationCommandID.XA_SYSTEMSESSION_DATA_CACHE, _handleSystemDataCommand);
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

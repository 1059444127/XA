using UIH.Mcsf.Core;
using UIH.Mcsf.Log;
using UIH.XR.Core;

namespace UIH.XR.AppManager
{
    public class AppManagerContainee : CLRContaineeBase
    {
        public override void DoWork()
        {
            IRemoteMethodInvoker remoteInvoker = _bootstrapper.AppContext.Container.GetExportedValue<IRemoteMethodInvoker>();
            _appManager = new AppManager();
            _appManager.Initialize(remoteInvoker);
            _iCommunicationProxy.RegisterEventHandler(ChannelID, AppReadyEventID, new StringEventHandler(HandleAppReadyEvent));
        }

        public override void Startup()
        {
            base.Startup();
            _iCommunicationProxy = GetCommunicationProxy();
            _bootstrapper = new XBootstrapper(appCfgPath, _iCommunicationProxy);
            _bootstrapper.Run();
        }

        private XBootstrapper _bootstrapper;
        private string appCfgPath = mcsf_clr_systemenvironment_config.GetApplicationPath() + @"xsample\config\AppManagerSample.xml";
        private ICommunicationProxy _iCommunicationProxy;
        private AppManager _appManager;

        private static int AppReadyEventID = 50011;//待systemmanager定义
        private static int ChannelID = 16;//待systemmanager定义

        private void HandleAppReadyEvent(string sender, string content)
        {
            bool result= _appManager.Invoke("ready", new object());
            CLRLogger.GetInstance().LogDevInfo(string.Format("All apps are ready,Sender is {0}, Content = {1}, invoke result:{2},curretn procedure:{3}",sender, content, result, _appManager.CurrentProcedure));
        }
    }
}

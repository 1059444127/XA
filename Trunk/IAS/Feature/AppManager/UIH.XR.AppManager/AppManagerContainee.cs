using UIH.Mcsf.Core;
using System;
using UIH.Mcsf.Log;

namespace UIH.XR.AppManager
{
    public class AppManagerContainee : CLRContaineeBase
    {
        public override void DoWork()
        {
            try
            {
                Console.WriteLine("appmanager DoWork begin");
                GetCommunicationProxy().RegisterEventHandler(ChannelID, AppReadyEventID, new StringEventHandler(HandleAppReadyEvent));
            }
            catch (Exception ex)
            {
                CLRLogger.GetInstance().LogDevError("DoWork error:" + ex.Message);
                Console.WriteLine("AppManagerContainee DoWork ex:" + ex.Message);
            }
        }

        public override void Startup()
        {
            try
            {
                Console.WriteLine("AppManagerContainee Startup begin");
                base.Startup();
            }
            catch (Exception ex)
            {
                CLRLogger.GetInstance().LogDevError("Startup error:"+ex.Message);
                Console.WriteLine("AppManagerContainee Startup ex:" + ex.Message);
            }
        }

        private static int AppReadyEventID = 50011;//待systemmanager定义
        private static int ChannelID = 50012;//待systemmanager定义

        private void HandleAppReadyEvent(string sender, string content)
        {
            Console.WriteLine(string.Format("All apps are ready,Sender is {0}, Content = {1}", sender, content));
            CLRLogger.GetInstance().LogDevInfo(string.Format("All apps are ready,Sender is {0}, Content = {1}", sender, content));
            AppManager appManager = new AppManager(GetCommunicationProxy());
            appManager.Initialize();
            appManager.Invoke("ready", null);
        }
    }
}

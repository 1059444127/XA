
using UIH.Mcsf.Core;
using System;
using UIH.Mcsf.Log;

namespace UIH.XR.AppManager
{
    public class AppManagerContainee : CLRContaineeBase
    {
        public AppManager appManager;

        public override void DoWork()
        {
            try
            {
                Console.WriteLine("appmanager DoWork begin");
                if (CheckAppsReady())
                {
                    CLRLogger.GetInstance().LogDevInfo("All apps are ready.");
                    appManager.Invoke("ready", null);
                }
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
                appManager = new AppManager(GetCommunicationProxy());
                appManager.Initialize();
                base.Startup();
            }
            catch (Exception ex)
            {
                CLRLogger.GetInstance().LogDevError("Startup error:"+ex.Message);
                Console.WriteLine("AppManagerContainee Startup ex:" + ex.Message);
            }
        }

        /// <summary>
        /// //所有的app启动完成后，由systemmanager广播消息，appmanager接受到消息返回ture
        /// </summary>
        /// <returns>true:all apps are ready
        ///          false：all apps are not ready
        /// </returns>
        private bool CheckAppsReady()
        {
            Console.WriteLine("AppManagerContainee CheckAppsReady ok");
            return true;
        }
    }
}


using UIH.XR.Core;
using UIH.Mcsf.Core;
using System;
using UIH.Mcsf.Log;

namespace UIH.XR.AppManager
{
    public class AppManagerContainee : CLRContaineeBase,IAppReady
    {
        private const int WAIT_TIME = 10000;//ms

        public AppManager appManager;

        public override void DoWork()
        {
            try
            {
                Console.WriteLine("appmanager DoWork begin");
                if (CheckAppsReady())
                {
                    CLRLogger.GetInstance().LogDevInfo("All apps are ready.");
                  //  appManager.Invoke("ready", null);
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
                appManager.Invoke("ready", null);
                base.Startup();
            }
            catch (Exception ex)
            {
                CLRLogger.GetInstance().LogDevError("Startup error:"+ex.Message);
                Console.WriteLine("AppManagerContainee Startup ex:" + ex.Message);
            }
        }

        private bool paReady = false;
        private bool examReady = false;
        string examReceiver =""; 
        string paReceiver = "";

        private bool CheckAppsReady()//如何异步实现每个app的检测？
        {
            //for (int i = WAIT_TIME; i >= 0; i -= 200)
            //{
            //    if (AppReady(examReceiver))
            //    {
            //        examReady = true;
            //        break;
            //    }
            //    Thread.Sleep(200);
            //}

            //for (int i = WAIT_TIME; i >= 0; i -= 200)
            //{
            //    if (AppReady(paReceiver))
            //    {
            //        paReady = true;
            //        break;
            //    }
            //    Thread.Sleep(200);
            //}

            //return paReady && examReady;
            Console.WriteLine("AppManagerContainee CheckAppsReady ok");
            return true;
        }

        public bool AppReady(string receiver)
        {
            return (bool)appManager._xshellManager._remoteInvoker.RemoteInvoke(receiver, new object());
        }
    }
}

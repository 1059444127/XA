
using UIH.XR.Core;
using UIH.Mcsf.Core;
using System;
using UIH.Mcsf.Log;

namespace UIH.XR.AppManager
{
    public class AppManagerContainee : CLRContaineeBase, IAppReady
    {
        private const int WAIT_TIME = 10000;//ms
        IRemoteMethodInvoker _remoteInvoker;
        public override void DoWork()
        {
            try
            {
                Console.WriteLine("appmanager DoWork begin");
                if (CheckAppsReady())
                {
                    CLRLogger.GetInstance().LogDevInfo("All apps are ready.");
                    _appManager.Invoke("ready", null);
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
                Console.WriteLine("appmanager Startup begin");

                _appManager = new AppManager();

                string appCfgPath = mcsf_clr_systemenvironment_config.GetApplicationPath() + @"xsample\config\AppManagerSample.xml";
                XBootstrapper _bootstrapper = new XBootstrapper(appCfgPath, GetCommunicationProxy());
                _bootstrapper.Run();

                 _remoteInvoker = _bootstrapper.AppContext.Container.GetExportedValue<IRemoteMethodInvoker>();
                 if (_remoteInvoker == null)
                {
                    CLRLogger.GetInstance().LogDevInfo("Begin Startup,_remoteInvoker is null.");
                    Console.WriteLine("_remoteInvoker is null");
                }

                _remoteInvoker.RegisterServiceObject<IShellManager>(new XShellManager());
                _remoteInvoker.RegisterServiceObject<IWorkflow>(_appManager);

                base.Startup();
            }
            catch (Exception ex)
            {
                CLRLogger.GetInstance().LogDevError("Startup error:"+ex.Message);
                Console.WriteLine("AppManagerContainee Startup ex:" + ex.Message);
            }
        }

        private AppManager _appManager;

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
            Console.WriteLine("appmanager CheckAppsReady ok");
            return true;
        }

        public bool AppReady(string receiver)
        {
            return (bool)_remoteInvoker.RemoteInvoke(receiver, new object());
        }
    }
}

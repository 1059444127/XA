
using System.ComponentModel.Composition;
using UIH.XR.Core;
using System.Threading;

namespace UIH.XR.AppManager
{
    public class AppManagerContainee : IAppReady
    {
        [Import(typeof(IRemoteMethodInvoker))]
        private IRemoteMethodInvoker iRemoteMethodInvoker { get; set; }

        private const int WAIT_TIME = 10000;//ms

        public void DoWork()
        {
            if (CheckAppsReady())
            {
                appManager.Invoke("ready", null);
            }
        }

        public AppManagerContainee(AppManager iAppManager)
        {
            appManager = iAppManager;
            iRemoteMethodInvoker.RegisterServiceObject<IShellManager>(new XShellManager());
            iRemoteMethodInvoker.RegisterServiceObject<IAppManager>(appManager);
        }

        private AppManager appManager;

        private bool paReady = false;
        private bool examReady = false;
        string examReceiver = UIH.XR.Core.Properties.CommunicationResource.CommunicationNode_Exam;
        string paReceiver = UIH.XR.Core.Properties.CommunicationResource.CommunicationNode_PA;

        private bool CheckAppsReady()//如何异步实现每个app的检测？
        {
            for (int i = WAIT_TIME; i >= 0; i -= 200)
            {
                if (AppReady(examReceiver))
                {
                    examReady = true;
                    break;
                }
                Thread.Sleep(200);
            }

            for (int i = WAIT_TIME; i >= 0; i -= 200)
            {
                if (AppReady(paReceiver))
                {
                    paReady = true;
                    break;
                }
                Thread.Sleep(200);
            }

            return paReady && examReady;
        }

        public bool AppReady(string receiver)
        {
            return (bool)iRemoteMethodInvoker.RemoteInvoke(receiver, new object());
        }
    }
}

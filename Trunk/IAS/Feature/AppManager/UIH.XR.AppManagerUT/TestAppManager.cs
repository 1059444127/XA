using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIH.Mcsf.Core;
using UIH.XR.AppManager;

namespace UIH.XR.AppManagerUT
{
    [TestClass]
    public class TestAppManager
    {
        UIH.XR.AppManager.AppManager _appmanager;

        [TestInitialize]
        public void TestAppManagerClass()
        {
            CLRCommunicationProxy icommunicationProxy = new CLRCommunicationProxy();
            icommunicationProxy.SetName("AppManager");

            if (icommunicationProxy.CheckCastToSystemDispatcher("127.0.0.1:10000") == 0)
            {
                icommunicationProxy.StartListener("");
                icommunicationProxy.SetSenderChannelId(12);
            }
            _appmanager = new AppManager.AppManager(icommunicationProxy);
            Assert.IsNotNull(_appmanager._communicationProxy);
        }

        [TestMethod]
        public void TestStartup()
        {
            AppManagerContainee _appManagerContainee = new AppManagerContainee();
            _appManagerContainee.Startup();
            Assert.IsNotNull(_appManagerContainee);
        }

        [TestMethod]
        public void TestInitialize()
        {
            Assert.IsNull(_appmanager._xshellManager._remoteInvoker);
            _appmanager.Initialize();
            Assert.IsNotNull(_appmanager._xshellManager._remoteInvoker);
        }

        [TestMethod]
        public void TestInvoke()
        {
            _appmanager.Invoke("ready", new object());
            bool result = _appmanager.Invoke("emergency", new object());
            Assert.IsTrue(result);
            result = _appmanager.Invoke("111", new object());
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestGetShell()
        {
            Assert.IsNull(XShellManager.GetInstance().GetShell(""));
        }
    }
}

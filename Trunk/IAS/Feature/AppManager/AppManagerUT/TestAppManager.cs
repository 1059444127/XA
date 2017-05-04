using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIH.XA.AppManager;

namespace UIH.XA.AppManagerUT
{
    [TestClass]
    public class TestAppManager
    {
        AppManagerContainee _appManagerContainee;

        [TestInitialize]
        public void TestAppManagerContaineeClass()
        {
            _appManagerContainee = new AppManagerContainee();
            Assert.IsNotNull(_appManagerContainee);
            _appManagerContainee.Startup();
            _appManagerContainee.DoWork();
        }

        [TestMethod]
        public void TestGetShell()
        {
            Assert.IsNull(XShellManager.GetInstance().GetShell(""));
        }
    }
}

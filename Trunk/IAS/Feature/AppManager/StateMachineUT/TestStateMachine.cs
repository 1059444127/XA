using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIH.XA.StateMachine.Default;
using System.Reflection;
using System.IO;
using UIH.XA.StateMachine;

namespace UIH.XA.StateMachineUT
{
    [TestClass]
    public class TestStateMachine
    {
        DefaultStateMachine _sm;

        [TestInitialize]
        public void TestInstantiate()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream fs = asm.GetManifestResourceStream("UIH.XA.StateMachineUT.SimpleStateMachine.xml");
            IStateMachine sm = new DefaultStateMachineFactory().CreateStateMachineFromXml(fs);
            _sm = sm as DefaultStateMachine;
            Assert.IsNotNull(_sm);
        }

        [TestMethod]
        public void TestTransact()
        {
            Assert.AreEqual(_sm.CurrentState.ID, "Initial");
            _sm.Transit("ready", null);
            Assert.AreEqual(_sm.CurrentState.ID, "Idle");
            _sm.Transit("pa", null);
            Assert.AreEqual(_sm.CurrentState.ID, "InPA");
            _sm.Transit("load study", null);
            Assert.AreEqual(_sm.CurrentState.ID, "InExam");
            _sm.Transit("pa", null);
            Assert.AreEqual(_sm.CurrentState.ID, "InPA");
            _sm.Transit("froze", null);
            Assert.AreEqual(_sm.CurrentState.ID, "InPA");
        }

        [TestMethod]
        public void TestToggleGateStateMachine()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream fs = asm.GetManifestResourceStream("UIH.XA.StateMachineUT.ToggleGateStateMachine.xml");
            IStateMachine sm = new DefaultStateMachineFactory().CreateStateMachineFromXml(fs);
            Assert.IsNotNull(sm as DefaultStateMachine);
            Assert.AreEqual(sm.CurrentState.ID, "Initial");
            sm.Transit("start", null);
            Assert.AreEqual(sm.CurrentState.ID, "Close");
            sm.Transit("open", null);
            Assert.AreEqual(sm.CurrentState.ID, "Open");
            sm.Transit("close", null);
            Assert.AreEqual(sm.CurrentState.ID, "Close");
        }
    }
}

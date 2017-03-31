using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XR.StateMachine.Default;
using System.Reflection;
using System.IO;
using UIH.XR.StateMachine;

namespace PerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Program app = new Program();
            app.TestInstantiate();
            app.PressureTest();
        }

        DefaultStateMachine _sm;

        public void TestInstantiate()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream fs = asm.GetManifestResourceStream("PerformanceTest.SimpleStateMachine.xml");
            IStateMachine sm = new DefaultStateMachineFactory().CreateStateMachineFromXml(fs);
            _sm = sm as DefaultStateMachine;
        }

        public void PressureTest()
        {
            int n = 1000 * 1000 * 1000;
            Random r = new Random();
            string[] cmds = { "ready", "pa", "load study", "froze", "begin exposure", "end exposure", "close", "unknown", "exam" };
            for (int i = 0; i < n; i++)
            {
                _sm.Transact(cmds[r.Next(cmds.Length)], null);
            }
        }
    }
}

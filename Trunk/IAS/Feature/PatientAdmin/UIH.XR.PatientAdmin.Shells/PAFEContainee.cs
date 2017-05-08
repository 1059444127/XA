using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UIH.Mcsf.Core;
using UIH.XA.Core;
using UIH.XA.PAUtilityCSharp.Communication;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PatientAdmin.Shells
{
    public class PAFEContainee : CLRContaineeBase
    {
        public ContaineeUtility _Containee = new ContaineeUtility();

        public override void Startup()
        {
            GlobalDefinition.LoggerWrapper.LogDevInfo("Start up");
        }

        public override void DoWork()
        {
            GlobalDefinition.LoggerWrapper.LogDevInfo("DoWork");

            System.Diagnostics.Debugger.Launch();
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Thread workerThread = new Thread(InitApp);

            workerThread.SetApartmentState(ApartmentState.STA);
            workerThread.Start(workerThread);
        }

        private void InitApp(object thread)
        {
            CommunicationSender sender = new CommunicationSender();

            Init(thread as Thread, GetCommunicationProxy(), sender);
        }

        private void Init(
            Thread mainThread,
            ICommunicationProxy commProxy, ICommunicationMap commMap)
        {
            string appCfgPath = @"D:\X-SW\XA\Trunk\UIH\appdata\patientadmin\config\PAShellWindowConfigure.xml";
            var app = new XApp(appCfgPath, commProxy);
            GlobalDefinition.Main = this;
            GlobalDefinition.MainWnd = app.MainWindow;
            //GlobalDefinition.MainDataContext = app.MainWindow.DataContext;
            GlobalDefinition.MainThread = mainThread;
            try
            {
                _Containee.Register(commProxy, commMap);
            }
            catch (System.Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.StackTrace);
            }
            app.Run();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            GlobalDefinition.LoggerWrapper.LogDevError("AppDomain has encountered a problem and needs to close.");

            var ex = (Exception)e.ExceptionObject;
            GlobalDefinition.LoggerWrapper.LogException(ex);

            throw ex;
        }

    }
}

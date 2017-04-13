using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Core;
using UIH.XR.Core;
using System.Diagnostics;
using UIH.Mcsf.Log;

namespace UIH.XR.XSample
{
    public class XSampleContainee: CLRContaineeBase
    {
        public override void DoWork()
        {
            string appCfgPath = mcsf_clr_systemenvironment_config.GetApplicationPath() + @"\xsample\config\XSample.xml";
            XApp app = new XApp(appCfgPath, GetCommunicationProxy());
            app.Run();            
        }

        public override void Startup()
        {
            base.Startup();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Logging;
using UIH.Mcsf.Log;
using UIH.Mcsf.Core;
using System.Diagnostics;

namespace UIH.XR.Core
{
    class XBootLogger : ILoggerFacade
    {
        private static string _configPath = Properties.Resources.LoggerConfigPath;

        private XBootLogger() { }

        private static XBootLogger _instance = new XBootLogger();

        public static XBootLogger Instance { get { return _instance; } }

        static XBootLogger()
        {
            //CLRLogger.GetInstance().CreateLogger(mcsf_clr_systemenvironment_config.GetApplicationPath() + _configPath);
        }

        public void Log(string message, Category category, Priority priority)
        {
            CLRLogger.GetInstance().LogDevInfo(message);
        }
    }
}

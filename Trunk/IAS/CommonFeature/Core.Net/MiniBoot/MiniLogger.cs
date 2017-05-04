/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XA.Core;
using UIH.Mcsf.Log;
using System.ComponentModel.Composition;

namespace UIH.XA.MiniBoot
{
    [Export(typeof(ILogger))]
    public class MiniLogger:ILogger
    {

        public ulong LogUID { get; set; }


        public string LogSource { get; set; }


        public bool LogDevError(string description)
        {
            return LogConsole(LogSource, "Dev", "Error", description);
        }

        public bool LogDevInfo(string description)
        {
            return LogConsole(LogSource, "Dev", "Info", description);
        }

        public bool LogDevWarning(string description)
        {
            return LogConsole(LogSource, "Dev", "Warning", description);
        }

        public bool LogException(Exception ex)
        {
            return true;
        }

        public bool LogSvcError(string description)
        {
            return LogConsole(LogSource, "Svc", "Error", description);
        }

        public bool LogSvcInfo(string description)
        {
            return LogConsole(LogSource, "Svc", "Info", description);
        }

        public bool LogSvcWarning(string description)
        {
            return LogConsole(LogSource, "Svc", "Warning", description);
        }

        public bool LogTraceError(string description)
        {
            return LogConsole(LogSource, "Trace", "Error", description);
        }

        public bool LogTraceInfo(string description)
        {
            return LogConsole(LogSource, "Trace", "Info", description);
        }

        public bool LogTraceWarning(string description)
        {
            return LogConsole(LogSource, "Trace", "Warning", description);
        }

        private bool LogConsole(string loggerName, string logType, string logLevel, string description)
        {
            Console.WriteLine("{0}\t-{1}:\t{2}\t{3}", logType, logLevel, loggerName, description);
            return true;
        }

    }
}

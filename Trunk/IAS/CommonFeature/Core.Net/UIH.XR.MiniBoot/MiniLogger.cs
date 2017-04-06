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
using UIH.XR.Core;
using UIH.Mcsf.Log;
using System.ComponentModel.Composition;

namespace UIH.XR.MiniBoot
{
    [Export(typeof(ILogger))]
    public class MiniLogger:ILogger
    {

        public bool LogDevError(string format, params object[] args)
        {
            string description = string.Format(format, args);
            return LogConsole(LoggerName, "Dev", "Error", description);
        }

        public bool LogDevInfo(string format, params object[] args)
        {
            string description = string.Format(format, args);
            return LogConsole(LoggerName, "Dev", "Info", description);
        }

        public bool LogDevWarning(string format, params object[] args)
        {
            string description = string.Format(format, args);
            return LogConsole(LoggerName, "Dev", "Warning", description);
        }

        public bool LogException(Exception ex)
        {
            //return LogConsole(LoggerName, "Trace", "Warning", description);
            return true;
        }

        public bool LogSvcError(string format, params object[] args)
        {
            string description = string.Format(format, args);
            return LogConsole(LoggerName, "Svc", "Error", description);
        }

        public bool LogSvcInfo(string format, params object[] args)
        {
            string description = string.Format(format, args);
            return LogConsole(LoggerName, "Svc", "Info", description);
        }

        public bool LogSvcWarning(string format, params object[] args)
        {
            string description = string.Format(format, args);
            return LogConsole(LoggerName, "Svc", "Warning", description);
        }

        public bool LogTraceError(string format, params object[] args)
        {
            string description = string.Format(format, args);
            return LogConsole(LoggerName, "Trace", "Error", description);
        }

        public bool LogTraceInfo(string format, params object[] args)
        {
            string description = string.Format(format, args);
            return LogConsole(LoggerName, "Trace", "Info", description);
        }

        public bool LogTraceWarning(string format, params object[] args)
        {
            string description = string.Format(format, args);
            return LogConsole(LoggerName, "Trace", "Warning", description);
        }

        private bool LogConsole(string loggerName, string logType, string logLevel, string description)
        {
            Console.WriteLine("{0}\t-{1}:\t{2}\t{3}", logType, logLevel, loggerName, description);
            return true;
        }

        private ulong _logUID;
        public ulong LogUID
        {
            get
            {
                return _logUID;
            }
            set
            {
                _logUID = value ;
            }
        }

        private string _loggerName;
        public string LoggerName
        {
            get
            {
                return _loggerName;
            }
            set
            {
                _loggerName = value;
            }
        }
    }
}

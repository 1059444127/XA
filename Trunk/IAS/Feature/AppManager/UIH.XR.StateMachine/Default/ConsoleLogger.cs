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
using System.Diagnostics;

namespace UIH.XR.StateMachine.Default
{
    /// <summary>
    /// this ILogger implementation is used to write log to console if no logger is specified.
    /// </summary>
    class ConsoleLogger
    {
        public bool LogDevError(string description)
        {
            WriteLog(description);
            return true;
        }

        public bool LogDevInfo(string description)
        {
            WriteLog(description);
            return true;
        }

        public bool LogDevWarning(string description)
        {
            WriteLog(description);
            return true;
        }

        public bool LogSvcError(string description)
        {
            WriteLog(description);
            return true;
        }

        public bool LogSvcInfo(string description)
        {
            WriteLog(description);
            return true;
        }

        public bool LogSvcWarning(string description)
        {
            WriteLog(description);
            return true;
        }

        public bool LogTraceError(string description)
        {
            WriteLog(description);
            return true;
        }

        public bool LogTraceInfo(string description)
        {
            WriteLog(description);
            return true;
        }

        public bool LogTraceWarning(string description)
        {
            WriteLog(description);
            return true;
        }

        public bool LogException(Exception ex)
        {
            WriteLog(ex.Message + " " + ex.StackTrace.Replace("\r\n", "|").Replace("\n", "|"));
            return true;
        }

        public ulong LogUID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string LoggerName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private void WriteLog(string content)
        {
            try
            {
                string time = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                StackTrace stackTrace = new StackTrace();
                var frame1 = stackTrace.GetFrame(1);
                var frame2 = stackTrace.GetFrame(2);
                Console.WriteLine(
                    frame1.GetMethod().Name + "\t"
                    + time + "\t"
                    + frame2.GetMethod().Module + "\t"
                    + frame2.GetMethod().Name + "(...)\t"
                    + frame2.GetFileName() + "\t" + content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

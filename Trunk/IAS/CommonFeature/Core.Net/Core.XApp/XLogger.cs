/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------
using UIH.Mcsf.Log;
using UIH.Mcsf.Core;
using System.ComponentModel.Composition;
using System;

namespace UIH.XA.Core.XApp
{
    [Export(typeof(ILogger))]
    public class XLogger : ILogger
    {
        public string LogSource { get; set; }
 
        public ulong LogUID { get; set; }

        public bool LogDevInfo(string description)
        {
            return CLRLogger.GetInstance().LogDevInfo(LogSource, LogUID, description);
        }

        public bool LogDevWarning(string description)
        {
            return CLRLogger.GetInstance().LogDevWarning(LogSource, LogUID, description);
        }

        public bool LogDevError(string description)
        {
            return CLRLogger.GetInstance().LogDevError(LogSource, LogUID, description);
        }

        public bool LogSvcInfo(string description)
        {
            return CLRLogger.GetInstance().LogSvcInfo(LogSource, LogUID, description);
        }

        public bool LogSvcWarning(string description)
        {
            return CLRLogger.GetInstance().LogSvcWarning(LogSource, LogUID, description);
        }

        public bool LogSvcError(string description)
        {
            return CLRLogger.GetInstance().LogSvcError(LogSource, LogUID, description);
        }

        public bool LogTraceInfo(string description)
        {
            return CLRLogger.GetInstance().LogTraceInfo(LogSource, description);
        }

        public bool LogTraceWarning(string description)
        {
            return CLRLogger.GetInstance().LogTraceWarning(LogSource, description);
        }

        public bool LogTraceError(string description)
        {
            return CLRLogger.GetInstance().LogTraceError(LogSource,  description);
        }

        public bool LogException(Exception ex)
        {
            return CLRLogger.GetInstance().LogDevError(ex.Message + " " + ex.StackTrace.Replace("\r\n", "|").Replace("\n", "|"));
        }
    }
}

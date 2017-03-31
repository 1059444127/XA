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

namespace UIH.XR.Core.XApp
{
    [Export(typeof(ILogger))]
    public class XLogger : ILogger
    {
        private static string logClientConfig = "\\temp\\XR_Logger_Client.xml";//暂时定义一个log client配置文件

        private XLogger()
        {
            CLRLogger.GetInstance().CreateLogger(mcsf_clr_systemenvironment_config.GetApplicationPath() + logClientConfig);
        }

        public string LoggerName { get; set; }
 
        public ulong LogUID { get; set; }

        public bool LogDevInfo(string description)
        {
            return CLRLogger.GetInstance().LogDevInfo(description);
        }

        public bool LogDevWarning(string description)
        {
            return CLRLogger.GetInstance().LogDevWarning(description);
        }

        public bool LogDevError(string description)
        {
            return CLRLogger.GetInstance().LogDevError(description);
        }

        public bool LogSvcInfo(string description)
        {
            return CLRLogger.GetInstance().LogSvcInfo(description);
        }

        public bool LogSvcWarning(string description)
        {
            return CLRLogger.GetInstance().LogSvcWarning(description);
        }

        public bool LogSvcError(string description)
        {
            return CLRLogger.GetInstance().LogSvcError(description);
        }

        public bool LogTraceInfo(string description)
        {
            return CLRLogger.GetInstance().LogTraceInfo(description);
        }

        public bool LogTraceWarning(string description)
        {
            return CLRLogger.GetInstance().LogTraceWarning(description);
        }

        public bool LogTraceError(string description)
        {
            return CLRLogger.GetInstance().LogTraceError(description);
        }

        public bool LogException(Exception ex)
        {
            return CLRLogger.GetInstance().LogDevError(ex.Message + " " + ex.StackTrace.Replace("\r\n", "|").Replace("\n", "|"));
        }
    }
}

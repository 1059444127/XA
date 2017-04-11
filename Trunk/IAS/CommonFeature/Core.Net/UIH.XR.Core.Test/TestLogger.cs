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
using System.Diagnostics;
using System.ComponentModel.Composition;

namespace UIH.XR.Core.Test
{
    [Export(typeof(ILogger))]
    public class TestLogger:ILogger
    {
        public string LoggerName
        {
            get;
            set;
        }

        public ulong LogUID
        {
            get;
            set;
        }

        public bool LogDevInfo(string format, params object[] args)
        {
            string content = "Info:\t" + string.Format(format, args);
            Debug.WriteLine(content);
            return true;
        }

        public bool LogDevWarning(string format, params object[] args)
        {
            string content = "Warning:\t" + string.Format(format, args);
            Debug.WriteLine(content);
            return true;
        }

        public bool LogDevError(string format, params object[] args)
        {
            string content = "Error:\t" + string.Format(format, args);
            Debug.WriteLine(content);
            return true;
        }

        public bool LogSvcInfo(string format, params object[] args)
        {
            string content = "Info:\t" + string.Format(format, args);
            Debug.WriteLine(content);
            return true;
        }

        public bool LogSvcWarning(string format, params object[] args)
        {
            string content = "Warning:\t" + string.Format(format, args);
            Debug.WriteLine(content);
            return true;
        }

        public bool LogSvcError(string format, params object[] args)
        {
            string content = "Error:\t" + string.Format(format, args);
            Debug.WriteLine(content);
            return true;
        }

        public bool LogTraceInfo(string format, params object[] args)
        {
            string content = "Info:\t" + string.Format(format, args);
            Debug.WriteLine(content);
            return true;
        }

        public bool LogTraceWarning(string format, params object[] args)
        {
            string content = "Warning:\t" + string.Format(format, args);
            Debug.WriteLine(content);
            return true;
        }

        public bool LogTraceError(string format, params object[] args)
        {
            string content = "Error:\t" + string.Format(format, args);
            Debug.WriteLine(content);
            return true;
        }

        public bool LogException(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}

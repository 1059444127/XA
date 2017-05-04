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

namespace UIH.XA.Core.Test
{
    [Export(typeof(ILogger))]
    public class TestLogger:ILogger
    {
        public string LogSource
        {
            get;
            set;
        }

        public ulong LogUID
        {
            get;
            set;
        }

        public bool LogDevInfo(string description)
        {
            string content = "Info:\t" + description;
            Debug.WriteLine(content);
            return true;
        }

        public bool LogDevWarning(string description)
        {
            string content = "Warning:\t" + description;
            Debug.WriteLine(content);
            return true;
        }

        public bool LogDevError(string description)
        {
            string content = "Error:\t" + description;
            Debug.WriteLine(content);
            return true;
        }

        public bool LogSvcInfo(string description)
        {
            string content = "Info:\t" + description;
            Debug.WriteLine(content);
            return true;
        }

        public bool LogSvcWarning(string description)
        {
            string content = "Warning:\t" + description;
            Debug.WriteLine(content);
            return true;
        }

        public bool LogSvcError(string description)
        {
            string content = "Error:\t" + description;
            Debug.WriteLine(content);
            return true;
        }

        public bool LogTraceInfo(string description)
        {
            string content = "Info:\t" + description;
            Debug.WriteLine(content);
            return true;
        }

        public bool LogTraceWarning(string description)
        {
            string content = "Warning:\t" + description;
            Debug.WriteLine(content);
            return true;
        }

        public bool LogTraceError(string description)
        {
            string content = "Error:\t" + description;
            Debug.WriteLine(content);
            return true;
        }

        public bool LogException(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}

/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------
using System;

namespace UIH.XR.Core
{
    public interface ILogger
    {
        string LoggerName { get; set; }

        ulong LogUID { get; set; }

        bool LogDevInfo(string format, params object[] args);

        bool LogDevWarning(string format, params object[] args);

        bool LogDevError(string format, params object[] args);

        bool LogSvcInfo(string format, params object[] args);

        bool LogSvcWarning(string format, params object[] args);

        bool LogSvcError(string format, params object[] args);

        bool LogTraceInfo(string format, params object[] args);

        bool LogTraceWarning(string format, params object[] args);

        bool LogTraceError(string format, params object[] args);

        bool LogException(Exception ex);
    }
}

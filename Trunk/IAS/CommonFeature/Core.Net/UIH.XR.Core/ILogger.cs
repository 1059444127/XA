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

        bool LogDevInfo(string description);

        bool LogDevWarning(string description);

        bool LogDevError(string description);

        bool LogSvcInfo(string description);

        bool LogSvcWarning(string description);

        bool LogSvcError(string description);

        bool LogTraceInfo(string description);

        bool LogTraceWarning(string description);

        bool LogTraceError(string description);

        bool LogException(Exception ex);
    }
}

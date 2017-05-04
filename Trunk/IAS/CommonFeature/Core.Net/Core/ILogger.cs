/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------
using System;

namespace UIH.XA.Core
{
    public interface ILogger
    {
        /// <summary>
        /// Log Source Name
        /// </summary>
        string LogSource { get; set; }

        /// <summary>
        /// Log UID
        /// </summary>
        ulong LogUID { get; set; }

        /// <summary>
        /// Log
        /// Type:   Dev  
        /// Level:  Info
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns></returns>
        bool LogDevInfo(string description);

        /// <summary>
        /// Log
        /// Type:   Dev  
        /// Level:  Warning
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns></returns>
        bool LogDevWarning(string description);

        /// <summary>
        /// Log
        /// Type:   Dev  
        /// Level:  Error
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns></returns>
        bool LogDevError(string description);

        /// <summary>
        /// Log
        /// Type:   Svc  
        /// Level:  Info
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns></returns>
        bool LogSvcInfo(string description);

        /// <summary>
        /// Log
        /// Type:   Svc  
        /// Level:  Warning
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns></returns>
        bool LogSvcWarning(string description);

        /// <summary>
        /// Log
        /// Type:   Svc  
        /// Level:  Error
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns></returns>
        bool LogSvcError(string description);

        /// <summary>
        /// Log
        /// Type:   Trace  
        /// Level:  Info
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns></returns>
        bool LogTraceInfo(string description);

        /// <summary>
        /// Log
        /// Type:   Trace  
        /// Level:  Warning
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns></returns>
        bool LogTraceWarning(string description);

        /// <summary>
        /// Log
        /// Type:   Trace  
        /// Level:  Error
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns></returns>
        bool LogTraceError(string description);

        /// <summary>
        /// Log Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        bool LogException(Exception ex);
    }
}

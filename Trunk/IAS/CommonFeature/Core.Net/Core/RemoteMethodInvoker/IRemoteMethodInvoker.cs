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
using System.Reflection;

namespace UIH.XA.Core
{
    /// <summary>
    /// RMI Service
    /// </summary>
    public interface IRemoteMethodInvoker : IRemoteMethodInvokerClient, IRemoteMethodInvokerServer
    {
    }
}

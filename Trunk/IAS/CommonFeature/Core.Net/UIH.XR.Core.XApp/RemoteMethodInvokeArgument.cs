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

namespace UIH.XR.Core.XApp
{
    [Serializable]
    public class RemoteMethodInvokeArgument
    {
        public MethodBase Method { get; private set; }

        public object[] Parameters { get; private set; }

        public string ObjectName { get; private set; }

        public RemoteMethodInvokeArgument(string objectName, MethodBase method, object[] parameters)
        {
            Method = method;
            Parameters = parameters;
            ObjectName = objectName;
        }
    }
}

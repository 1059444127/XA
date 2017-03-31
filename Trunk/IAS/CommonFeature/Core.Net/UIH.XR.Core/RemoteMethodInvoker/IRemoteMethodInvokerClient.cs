using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UIH.XR.Core
{
    public interface IRemoteMethodInvokerClient
    {
        /// <summary>
        /// Invoke the method specified by MethodBase instance on remote target
        /// </summary>
        /// <param name="receiver">the target communication point holds the method</param>
        /// <param name="parameters">method parameters</param>
        /// <returns>object returned by remote target</returns>
        object RemoteInvoke(string receiver, params object[] parameters);

        /// <summary>
        /// Invoke the calling method on remoteTarget
        /// </summary>
        /// <param name="receiver">the target communication point holds the method</param>
        /// <param name="method">method specification</param>
        /// <param name="parameters">method parameters</param>
        /// <returns>object returned by remote target</returns>
        object RemoteInvoke(string receiver, MethodBase method, params object[] parameters);

        /// <summary>
        /// Invoke the calling method on remoteTarget
        /// </summary>
        /// <param name="receiver">the target communication point holds the method</param>
        /// <param name="serviceObjectName">the name of target service object which the method invoked on</param>
        /// <param name="method">method specification</param>
        /// <param name="parameters">method parameters</param>
        /// <returns>object returned by remote target</returns>
        object RemoteInvoke(string receiver, string serviceObjectName, MethodBase method, params object[] parameters);
    }
}

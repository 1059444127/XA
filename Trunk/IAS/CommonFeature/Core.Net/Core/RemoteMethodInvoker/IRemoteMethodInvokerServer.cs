using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.XA.Core
{
    public interface IRemoteMethodInvokerServer
    {
        /// <summary>
        /// Register an object to make it can be invoked by remote.
        /// And the method parameter should be serializable.
        /// The service object has the name equals typeof(T).FullName if name is null or empty
        /// </summary>
        /// <param name="obj">the service object</param>
        /// <param name="name">the name of service object as an identifier</param>
        void RegisterServiceObject<T>(T obj, string name = null) where T : class;

        /// <summary>
        /// GetObject current remote invoker which communicates with me.
        /// </summary>
        /// <returns>remote invoker's name</returns>
        string GetCurrentRemoteInvoker();
    }
}

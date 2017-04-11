using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition.Hosting;

namespace UIH.XR.Core
{
    public interface IAppContext
    {
        /// <summary>
        /// DefaultCLRCommunicationProxy
        /// </summary>
        object DefaultCLRCommunicationProxy { get; }

        /// <summary>
        /// Container instance
        /// </summary>
        CompositionContainer Container { get; }

        /// <summary>
        /// Get export instance by contract name 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectName"></param>
        /// <returns></returns>
        T GetObject<T>(string objectName);

        /// <summary>
        /// Get export instance by type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetObject<T>();



    }
}

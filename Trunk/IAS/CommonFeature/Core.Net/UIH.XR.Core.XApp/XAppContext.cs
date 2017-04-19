using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace UIH.XR.Core.XApp
{
    [Export(typeof(IAppContext))]
    public class XAppContext : IAppContext
    {
        #region [---Fields---]

        private readonly ConcurrentDictionary<string, object> _concurrentDictionary = new ConcurrentDictionary<string, object>();

        #endregion


        #region Implementation of IAppContext

        public T GetObject<T>(string objName)
        {
            object result;

            if (this._concurrentDictionary.TryGetValue(objName, out result))
            {
                return (T)result;
            }
            else
            {
                //todo:
                return default(T);
            }
        }

        public bool AddObject<T>(string objName, T objInstance)
        {
            var obj = this._concurrentDictionary.AddOrUpdate(objName, objInstance, (key, value) => objInstance);
            if (null != obj)
            {
                return true;
            }
            else
            {
                //todo:
                return false;
            }
        }

        public bool RemoveObject<T>(string objName)
        {
            object obj;
            if (this._concurrentDictionary.TryRemove(objName, out obj))
            {
                return true;
            }
            else
            {
                //todo;
                return false;
            }
        }


        #endregion
    }
}

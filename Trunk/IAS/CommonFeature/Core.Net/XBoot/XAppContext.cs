//////////////////////////////////////////////////////////////////////////
/// Copyright, (c) Shanghai United Imaging Healthcare Inc., 2011
/// All rights reserved.
///
/// \author Jinyang Li     jinyang.li@united-imaging.com
//
/// \file XAppContext.cs
///
/// \brief  This Class is Generalize the Interface IAppContext
///
/// \version 1
/// \date Apr  , 2017
/////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace UIH.XA.Core
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
        #endregion



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
    }
}

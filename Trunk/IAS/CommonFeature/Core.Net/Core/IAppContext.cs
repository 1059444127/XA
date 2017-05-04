//////////////////////////////////////////////////////////////////////////
/// Copyright, (c) Shanghai United Imaging Healthcare Inc., 2011
/// All rights reserved.
///
/// \author Jinyang Li     jinyang.li@united-imaging.com
//
/// \file IAppContext.cs
///
/// \brief  This Interface is used to Get Application Context Object,and
/// the Object is added When Application is Running
///
/// \version 1
/// \date Apr  , 2017
/////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.XA.Core
{
    public interface IAppContext
    {
        /// <summary>
        /// Get a Application Context Object by Name
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="objName">Object Name</param>
        /// <returns></returns>
        T GetObject<T>(string objName);
    }
}

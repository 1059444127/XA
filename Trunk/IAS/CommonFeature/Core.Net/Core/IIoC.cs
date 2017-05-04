//////////////////////////////////////////////////////////////////////////
/// Copyright, (c) Shanghai United Imaging Healthcare Inc., 2011
/// All rights reserved.
///
/// \author Jinyang Li     jinyang.li@united-imaging.com
//
/// \file IIoc.cs
///
/// \brief  This Interface Defines Get Instance From Mef Container,that if the Object is Generate by Mef Container,
/// you can use the Interface,other not
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
    public interface IIoC<TContainer>
    {
        /// <summary>
        /// The Mef Container
        /// </summary>
        TContainer Container { get; set; }

        /// <summary>
        /// Get Object from Mef Container
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="contractType">Object Contract Type</param>
        /// <param name="contractName">Object Contract Name</param>
        /// <returns></returns>
        T Get<T>(Type contractType, string contractName = "");

        /// <summary>
        /// Lazied Get Object
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="contractType">Object Contract Type</param>
        /// <param name="contractName">Object Contract Name</param>
        /// <returns></returns>
        Lazy<T> GetLazy<T>(Type contractType, string contractName = "");
    }
}

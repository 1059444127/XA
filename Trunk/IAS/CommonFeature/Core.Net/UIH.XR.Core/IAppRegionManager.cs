//////////////////////////////////////////////////////////////////////////
/// Copyright, (c) Shanghai United Imaging Healthcare Inc., 2011
/// All rights reserved.
///
/// \author Jinyang Li     jinyang.li@united-imaging.com
//
/// \file IAppRegionManager.cs
///
/// \brief  This Interface Defines associate UI Control to the placeHolder "Region"
///
/// \version 1
/// \date Apr  , 2017
/////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.XR.Core
{
    public interface IAppRegionManager
    {
        /// <summary>
        /// Register the UI Control to the Region
        /// </summary>
        /// <param name="regionName">placeholder name</param>
        /// <param name="viewType">the UI Control Type</param>
        void RegisterViewToRegion(string regionName, Type viewType);

        /// <summary>
        /// Register the UI Control to the Region
        /// </summary>
        /// <param name="regionName">placeholder name</param>
        /// <param name="getViewDelegate">the delegate which to generate a UI Control Object</param>
        void RegisterViewToRegion(string regionName, Func<object> getViewDelegate);

        /// <summary>
        /// Add a UI Control Object to the region
        /// </summary>
        /// <param name="regionName">placeholder name</param>
        /// <param name="view">UI Control Object</param>
        void AddViewToRegion(string regionName, object view);

        /// <summary>
        /// Navigate to other UI Control
        /// </summary>
        /// <param name="regionName">placeholder name</param>
        /// <param name="viewContractName">Exported UI Control Object Contract Name</param>
        void NavigateTo(string regionName, string viewContractName);
    }
}

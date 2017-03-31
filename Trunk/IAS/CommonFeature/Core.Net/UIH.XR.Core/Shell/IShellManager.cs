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

namespace UIH.XR.Core
{
    public interface IShellManager
    {
        /// <summary>
        /// Register shell
        /// </summary>
        /// <param name="shell"></param>
        /// <returns></returns>
        bool RegisterShell(IShell shell);

        /// <summary>
        /// Unregister shell
        /// </summary>
        /// <param name="shell"></param>
        /// <returns></returns>
        bool UnregisterShell(IShell shell);

    }
}

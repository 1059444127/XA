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
    public interface IShell
    {
        /// <summary>
        /// GetObject ShellName
        /// </summary>
        string ShellName { get; set; }

        /// <summary>
        /// ShowShell
        /// </summary>
        /// <returns></returns>
        bool ShowShell();

        /// <summary>
        /// HideShell
        /// </summary>
        /// <returns></returns>
        bool HideShell();

        /// <summary>
        /// BlockShell
        /// </summary>
        /// <returns></returns>
        bool BlockShell();

        /// <summary>
        /// UnblockShell
        /// </summary>
        /// <returns></returns>
        bool UnblockShell();
    }
}

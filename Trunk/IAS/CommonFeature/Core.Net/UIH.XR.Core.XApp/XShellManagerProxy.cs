﻿/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------
using System.ComponentModel.Composition;
using UIH.XR.GlobalParameter;

namespace UIH.XR.Core.XApp
{
    [Export(typeof(IShellManager))]
    public class XShellManagerProxy : IShellManager
    {
        static string APP_MANAGER_NAME = CommunicationNode.AppManager;
        [Import]
        private IRemoteMethodInvoker RemoteMethodInvoker { get; set; }

        public bool RegisterShell(IShell shell)
        {
            return (bool)RemoteMethodInvoker.RemoteInvoke(APP_MANAGER_NAME, shell);
        }

        public bool UnregisterShell(IShell shell)
        {
            return (bool)RemoteMethodInvoker.RemoteInvoke(APP_MANAGER_NAME, shell);
        }
    }
}

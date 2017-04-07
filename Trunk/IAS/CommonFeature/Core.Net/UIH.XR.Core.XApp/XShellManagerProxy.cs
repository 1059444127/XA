/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------
using System.ComponentModel.Composition;
using UIH.XR.GlobalParameter;
using System.Collections.Generic;

namespace UIH.XR.Core.XApp
{
    [Export(typeof(IShellManager))]
    public class XShellManagerProxy : IShellManager
    {
        #region Properties

        static string APP_MANAGER_NAME = CommunicationNode.AppManager;
        [Import]
        private IRemoteMethodInvoker RemoteMethodInvoker { get; set; }

        /// <summary>
        /// Shell collection (local)
        /// </summary>
        protected Dictionary<string, IShell> ShellCollectin { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XShellManagerProxy()
        {
            ShellCollectin = new Dictionary<string, IShell>();
        }

        #endregion



        #region Methods

        /// <summary>
        /// RegisterShell
        /// </summary>
        /// <param name="shell"></param>
        /// <returns></returns>
        public bool RegisterShell(IShell shell)
        {
            XShellShadow shellShadow = new XShellShadow(shell.ShellName);
            bool result= (bool)RemoteMethodInvoker.RemoteInvoke(APP_MANAGER_NAME, shellShadow);

            if (result)
                AddShell(shell);

            return result;
        }

        /// <summary>
        /// UnregisterShell
        /// </summary>
        /// <param name="shell"></param>
        /// <returns></returns>
        public bool UnregisterShell(IShell shell)
        {
            XShellShadow shellShadow = new XShellShadow(shell.ShellName);
            bool result = (bool)RemoteMethodInvoker.RemoteInvoke(APP_MANAGER_NAME, shellShadow);

            if (result)
                RemoveShell(shell);

            return result;
        }

        /// <summary>
        /// Get Shell instance by shell name
        /// </summary>
        /// <param name="shellName"></param>
        /// <returns></returns>
        public IShell GetShell(string shellName)
        {
            if (ShellCollectin.ContainsKey(shellName))
                return ShellCollectin[shellName];

            return null;
        }

        /// <summary>
        /// Add shell into local collection. 
        /// If the shell is exsit, cover it with new shell. 
        /// </summary>
        /// <param name="shell"></param>
        private void AddShell(IShell shell)
        {
            if (ShellCollectin.ContainsKey(shell.ShellName))
                ShellCollectin[shell.ShellName] = shell;
            else
                ShellCollectin.Add(shell.ShellName, shell);
        }

        /// <summary>
        /// Remove shell from local collection
        /// </summary>
        /// <param name="shell"></param>
        private void RemoveShell(IShell shell)
        {
            if (ShellCollectin.ContainsKey(shell.ShellName))
                ShellCollectin.Remove(shell.ShellName);
        }

        #endregion
    }
}

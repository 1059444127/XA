using System.Collections.Generic;
using UIH.XR.Core;
using System.ComponentModel.Composition;

namespace UIH.XR.AppManager
{
    public class XShellManager:IShellManager
    {
        private IDictionary<string, IShell> _shellCollection;
        private IDictionary<string, IShell> ShellCollection
        {
            get { return _shellCollection; }
            set { _shellCollection = value; }
        }

        /// <summary>
        /// Construct XShellManager object
        /// </summary>
        public XShellManager()
        {
            _shellCollection = new Dictionary<string, IShell>();
        }

        /// <summary>
        /// Get shell by shellName
        /// </summary>
        /// <param name="shellName">shellName</param>
        /// <returns>success:shellname
        ///          fail:null
        /// </returns>
        public IShell GetShell(string shellName)
        {
            return ShellCollection.ContainsKey(shellName) ? ShellCollection[shellName] : null;
        }

        public bool RegisterShell(IShell shell)
        {
            ShellCollection.Add(shell.ShellName, shell);
            return true;
        }

        public bool UnregisterShell(IShell shell)
        {
            return ShellCollection.ContainsKey(shell.ShellName)?ShellCollection.Remove(shell.ShellName):false;
        }
    }
}

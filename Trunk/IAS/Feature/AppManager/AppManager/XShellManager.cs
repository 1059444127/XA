using System.Collections.Generic;
using UIH.XA.Core;

namespace UIH.XA.AppManager
{
    public class XShellManager:IShellManager
    {
        private static readonly object locker = new object();
        private volatile static XShellManager _xshellManager = null;
        private static IDictionary<string, IShell> _shellCollection;
        public IRemoteMethodInvoker _remoteInvoker{get;private set;}

        /// <summary>
        /// Initialize,used by AppManager
        /// </summary>
        /// <param name="communicationProxy"></param>
        public void Initialize(IRemoteMethodInvoker iRemoteMethodInvoker)
        {
            _remoteInvoker = iRemoteMethodInvoker;
            _remoteInvoker.RegisterServiceObject<IShellManager>(this);
        }

        /// <summary>
        /// consturct
        /// </summary>
        private XShellManager()
        {
            _shellCollection = new Dictionary<string, IShell>();
        }

        /// <summary>
        /// Get XShellManager instance
        /// </summary>
        /// <returns></returns>
        public static XShellManager GetInstance()
        {
            if (null==_xshellManager)
            {
                lock (locker)
                {
                    if (null==_xshellManager)
                    {
                        _xshellManager = new XShellManager();
                    }
                }
            }
            return _xshellManager;
        }

        /// <summary>
        /// return xshellproxy
        /// </summary>
        /// <param name="shellName">shellName</param>
        /// <returns>Ishellproxy</returns>
        public IShell GetShell(string shellName)
        {
            return _shellCollection.ContainsKey(shellName) ? _shellCollection[shellName] : null;
        }

        public bool RegisterShell(IShell shell)
        {
            if (!_shellCollection.ContainsKey(shell.ShellName))
            {
                _shellCollection.Add(shell.ShellName, new XShellProxy(_remoteInvoker, shell.ShellName, _remoteInvoker.GetCurrentRemoteInvoker()));
                return true;
            }
            return false;
        }

        public bool UnregisterShell(IShell shell)
        {
            return _shellCollection.ContainsKey(shell.ShellName) ? _shellCollection.Remove(shell.ShellName) : false;
        }
    }
}

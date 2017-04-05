using System.Collections.Generic;
using UIH.XR.Core;
using UIH.Mcsf.Core;
using System;
using UIH.Mcsf.Log;

namespace UIH.XR.AppManager
{
    public class XShellManager:IShellManager
    {
        private static readonly object lockHelper = new object();
        private string appCfgPath = mcsf_clr_systemenvironment_config.GetApplicationPath() + @"xsample\config\AppManagerSample.xml";
        private volatile static XShellManager _xshellManager = null;
        private static IDictionary<string, IShell> _shellCollection;

        public IRemoteMethodInvoker _remoteInvoker;

        public void Initialize(ICommunicationProxy communicationProxy)
        {
            Console.WriteLine("XShellManager Initialize");

            XBootstrapper _bootstrapper = new XBootstrapper(appCfgPath, communicationProxy);
            _bootstrapper.Run();
            _remoteInvoker = _bootstrapper.AppContext.Container.GetExportedValue<IRemoteMethodInvoker>();
            if (null == _remoteInvoker)
            {
                CLRLogger.GetInstance().LogDevInfo("Begin Startup,_remoteInvoker is null.");
                Console.WriteLine("_remoteInvoker is null");
                return;
            }
            _remoteInvoker.RegisterServiceObject<IShellManager>(this);
        }

        private XShellManager()
        {
            _shellCollection = new Dictionary<string, IShell>();
        }

        public static XShellManager GetInstance()
        {
            if (null==_xshellManager)
            {
                lock (lockHelper)
                {
                    if (null==_xshellManager)
                    {
                        Console.WriteLine("XShellManager construct");
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
             string appCommunicationProxyName = _remoteInvoker.GetCurrentRemoteInvoker();
             _shellCollection.Add(shell.ShellName, new XShellProxy(_remoteInvoker, shell.ShellName, appCommunicationProxyName));
             return true;
        }

        public bool UnregisterShell(IShell shell)
        {
            return _shellCollection.ContainsKey(shell.ShellName) ? _shellCollection.Remove(shell.ShellName) : false;
        }
    }
}

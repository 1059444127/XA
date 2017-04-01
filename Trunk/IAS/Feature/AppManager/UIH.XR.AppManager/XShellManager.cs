using System.Collections.Generic;
using UIH.XR.Core;
using UIH.Mcsf.Core;
using System;
using UIH.Mcsf.Log;

namespace UIH.XR.AppManager
{
    public class XShellManager:IShellManager
    {
        public IRemoteMethodInvoker _remoteInvoker;
        private string appCfgPath = mcsf_clr_systemenvironment_config.GetApplicationPath() + @"xsample\config\AppManagerSample.xml";
        private IDictionary<string, IShell> _shellCollection;
        private IDictionary<string, IShell> ShellCollection
        {
            get { return _shellCollection; }
            set { _shellCollection = value; }
        }

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
            }
           
            _remoteInvoker.RegisterServiceObject<IShellManager>(this);
        }

     
        public XShellProxy _xshellProxy;
        public XShellManager()
        {
            Console.WriteLine("XShellManager construct");
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
             _xshellProxy = new XShellProxy(_remoteInvoker,shell.ShellName,"");//需要根据shell来判断receiver
            return true;
        }

        public bool UnregisterShell(IShell shell)
        {
            return ShellCollection.ContainsKey(shell.ShellName)?ShellCollection.Remove(shell.ShellName):false;
        }
    }
}

using UIH.XR.Core;
using System;

namespace UIH.XR.AppManager
{
    public class XShellProxy :IShell
    {
        private IRemoteMethodInvoker _remoteInvoker;

        private string appCommunicationProxyName;

        public string ShellName { get; set; }

        public XShellProxy(IRemoteMethodInvoker remoteMethodInvoker, string shellName, string receiver)
        {
            _remoteInvoker = remoteMethodInvoker;
            appCommunicationProxyName = receiver;
            ShellName = shellName;
        }

        public IShell GetSerializableShadow()
        {
            throw new Exception();
        }

        public bool ShowShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(appCommunicationProxyName, this);
        }

        public bool HideShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(appCommunicationProxyName, this);
        }

        public bool BlockShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(appCommunicationProxyName, this);
        }

        public bool UnblockShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(appCommunicationProxyName, this);
        }
    }
}

using UIH.XA.Core;

namespace UIH.XA.AppManager
{
    public class XShellProxy :IShell
    {
        private IRemoteMethodInvoker _remoteInvoker;
        private string _receiver;
        public string ShellName { get; set; }

        public XShellProxy(IRemoteMethodInvoker remoteMethodInvoker, string shellName, string appCommunicationProxyName)
        {
            _remoteInvoker = remoteMethodInvoker;
            _receiver = appCommunicationProxyName;
            ShellName = shellName;
        }

        public bool ShowShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(_receiver, ShellName, this);
        }

        public bool HideShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(_receiver, ShellName, this);
        }

        public bool BlockShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(_receiver, ShellName, this);
        }

        public bool UnblockShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(_receiver, ShellName, this);
        }
    }
}

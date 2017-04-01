using UIH.XR.Core;
using System.Windows;

namespace UIH.XR.AppManager
{
    public class XShellProxy : Window,IShell
    {
        private IRemoteMethodInvoker _remoteInvoker;
        private string _xreceiver;

        public XShellProxy(IRemoteMethodInvoker remoteMethodInvoker, string shellName, string receiver)
        {
            _remoteInvoker = remoteMethodInvoker;
            _xreceiver = receiver;
            ShellName = shellName;
        }

        public string ShellName { get; set; }

        public IShell GetSerializableShadow()
        {
            return (IShell)_remoteInvoker.RemoteInvoke(_xreceiver,this);
        }

        public bool ShowShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(_xreceiver, this);
        }

        public bool HideShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(_xreceiver, this);
        }

        public bool BlockShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(_xreceiver, this);
        }

        public bool UnblockShell()
        {
            return (bool)_remoteInvoker.RemoteInvoke(_xreceiver, this);
        }
    }
}

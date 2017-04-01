using UIH.XR.Core;
using System.Windows;
using System.ComponentModel.Composition;

namespace UIH.XR.AppManager
{
    [Export(typeof(IShell))]
    public class XShellProxy : Window,IShell
    {
        [Import]
        private IRemoteMethodInvoker RemoteMethodInvoker { get; set; }

        private IShell shell;
        private XShellManager _xShellManager;
        private string _xreceiver;

        public XShellProxy(string shellName,string receiver)
        {
            _xreceiver = receiver;
            ShellName = shellName;
            _xShellManager = new XShellManager();
            shell = _xShellManager.GetShell(shellName);
        }

        public string ShellName { get; set; }

        public IShell GetSerializableShadow()
        {
            return null;
        }

        public bool ShowShell()
        {
            return (bool)RemoteMethodInvoker.RemoteInvoke(_xreceiver, shell);
        }

        public bool HideShell()
        {
            return (bool)RemoteMethodInvoker.RemoteInvoke(_xreceiver, shell);
        }

        public bool BlockShell()
        {
            return (bool)RemoteMethodInvoker.RemoteInvoke(_xreceiver, shell);
        }

        public bool UnblockShell()
        {
            return (bool)RemoteMethodInvoker.RemoteInvoke(_xreceiver, shell);
        }
    }
}

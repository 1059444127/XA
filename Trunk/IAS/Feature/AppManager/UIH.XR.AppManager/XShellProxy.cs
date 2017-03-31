using System;
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
        private XShellManager xShellManager;
        private string xreceiver;

        public XShellProxy(string shellName,string receiver)
        {
            xreceiver = receiver;
            ShellName = shellName;
            xShellManager = new XShellManager();
            shell = xShellManager.GetShell(shellName);
            this.Loaded += new RoutedEventHandler(XShell_Loaded);
            this.Closed += new EventHandler(XShell_Closed);
        }

        public string ShellName { get; set; }

        public IShell GetSerializableShadow()
        {
            return null;
        }

        public bool ShowShell()
        {
            return (bool)RemoteMethodInvoker.RemoteInvoke(xreceiver, shell);
        }

        public bool HideShell()
        {
            return (bool)RemoteMethodInvoker.RemoteInvoke(xreceiver, shell);
        }

        public bool BlockShell()
        {
            return (bool)RemoteMethodInvoker.RemoteInvoke(xreceiver, shell);
        }

        public bool UnblockShell()
        {
            return (bool)RemoteMethodInvoker.RemoteInvoke(xreceiver, shell);
        }

        public void XShell_Closed(object sender, EventArgs e)
        {
            RemoteMethodInvoker.RemoteInvoke(xreceiver, shell);
        }

        public void XShell_Loaded(object sender, RoutedEventArgs e)
        {
            RemoteMethodInvoker.RemoteInvoke(xreceiver, shell);
        }
    }
}

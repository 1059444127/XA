/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

namespace UIH.XR.Core.XApp
{
    public class XShell : Window, IShell
    {
        public string ShellName { get; private set; }

        public bool ShowShell()
        {
            this.Show();
            this.Activate();
            return true;
        }

        public bool HideShell()
        {
            this.Hide();
            return true;
        }

        public bool BlockShell()
        {
            this.IsEnabled = false;
            return true;
        }

        public bool UnblockShell()
        {
            this.IsEnabled = true;
            return true;
        }

        public XShell(string name)
        {
            ShellName = name;
        }


        public IShell GetSerializableShadow()
        {
            return new XShellShadow(ShellName);
        }
    }

    [Serializable]
    public class XShellShadow : IShell
    {
        public XShellShadow(string shellName)
        {
            ShellName = shellName;
        }

        public string ShellName { get; set; }

        public bool ShowShell()
        {
            throw new NotImplementedException();
        }

        public bool HideShell()
        {
            throw new NotImplementedException();
        }

        public bool BlockShell()
        {
            throw new NotImplementedException();
        }

        public bool UnblockShell()
        {
            throw new NotImplementedException();
        }


        public IShell GetSerializableShadow()
        {
            throw new NotImplementedException();
        }
    }
}

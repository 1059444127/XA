﻿/// ------------------------------------------------------------------------------
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
        public string ShellName { get; set; }

        public virtual bool ShowShell()
        {
            this.Show();
            this.Activate();
            return true;
        }

        public virtual bool HideShell()
        {
            this.Hide();
            return true;
        }

        public virtual bool BlockShell()
        {
            this.IsEnabled = false;
            return true;
        }

        public virtual bool UnblockShell()
        {
            this.IsEnabled = true;
            return true;
        }

    }

    
}

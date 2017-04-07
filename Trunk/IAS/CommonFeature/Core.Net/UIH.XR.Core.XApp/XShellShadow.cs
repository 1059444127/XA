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

namespace UIH.XR.Core.XApp
{
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
    }
}

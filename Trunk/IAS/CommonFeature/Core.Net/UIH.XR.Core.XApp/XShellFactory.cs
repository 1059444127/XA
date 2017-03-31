using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace UIH.XR.Core.XApp
{
    [Export(typeof(IShellFactory))]
    class XShellFactory : IShellFactory
    {
        public IShell CreateShell(string shellName)
        {
            return new XShell(shellName);
        }
    }
}

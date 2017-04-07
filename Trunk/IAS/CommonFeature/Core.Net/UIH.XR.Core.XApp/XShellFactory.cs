using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace UIH.XR.Core.XApp
{
    [System.Obsolete("已经不在使用")]  
    [Export(typeof(IShellFactory))]
    class XShellFactory : IShellFactory
    {
        public IShell CreateShell(string shellName)
        {
            return new XShell();
        }
    }
}

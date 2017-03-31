using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.XR.Core
{
    public interface IShellFactory
    {
        IShell CreateShell(string shellName);
    }
}

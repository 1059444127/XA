using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XA.PAUtilityCSharp.Enumeration;

namespace UIH.XA.PAUtilityCSharp.Models
{
    public interface ICommonProperty
    {
        Protect Protect { get; set; }

        string InstanceUID { get; set; }
    }
}

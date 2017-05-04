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

namespace UIH.XA.XSample.BusinessLogicInterface
{

    [Serializable]
    public class PatientRemoteObject
    {
        public string InvokeTag { get; set; }

        public object[] Paramters { get; set; }

        public object Result { get; set; }
    }
}

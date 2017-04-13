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

namespace UIH.XR.XSample.BusinessLogicInterface
{
    [Serializable]
    public class Patient
    {
        public ulong PatientID { get; set; }

        public string PatientName { get; set; }

        public uint PatientAge { get; set; }

        public Sex PatientSex { get; set; }

        public Patient Copy()
        {
            return new Patient() { PatientID = PatientID, PatientName = PatientName, PatientAge = PatientAge, PatientSex = PatientSex };
        }
    }

    [Serializable]
    public enum Sex
    {
        Male,
        Female
    }
}

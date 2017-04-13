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
using System.ComponentModel.Composition;

namespace UIH.XR.XSample.BusinessLogicInterface
{
    public interface IPatientSource
    {
        bool NewPatient(Patient patient);

        bool DeletePatient(ulong patientID);

        bool ModifyPatient(Patient patient);

        IList<Patient> GetPatients();
    }
}

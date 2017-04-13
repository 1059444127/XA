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
using UIH.XR.XSample.BusinessLogicInterface;

namespace UIH.XR.XSample.BusinessLogicExcutor
{
    public class PatientManager : IPatientSource
    {

        Dictionary<ulong, Patient> PatientCollection { get; set; }


        public PatientManager()
        {
            PatientCollection = new Dictionary<ulong, Patient>();

            NewPatient(new Patient { PatientName = "张三", PatientAge = 23, PatientSex = Sex.Male });
            NewPatient(new Patient { PatientName = "李四", PatientAge = 29, PatientSex = Sex.Female });
            NewPatient(new Patient { PatientName = "王五", PatientAge = 31, PatientSex = Sex.Male });
            NewPatient(new Patient { PatientName = "赵六", PatientAge = 25, PatientSex = Sex.Female });

        }

        public bool NewPatient(Patient patient)
        {
            patient.PatientID = GetNewPatientID();
            PatientCollection.Add(patient.PatientID, patient);
            PrintPatients();
            return true;
        }

        public bool DeletePatient(ulong patientID)
        {
            if (PatientCollection.Keys.Contains(patientID))
            {
                PatientCollection.Remove(patientID);
                PrintPatients();
            }
            return true;
        }

        public bool ModifyPatient(Patient patient)
        {

            if (PatientCollection.Keys.Contains(patient.PatientID))
            {
                Patient oldPatient = PatientCollection[patient.PatientID];
                oldPatient.PatientName = patient.PatientName;
                oldPatient.PatientAge = patient.PatientAge;
                oldPatient.PatientSex = patient.PatientSex;
                PatientCollection[patient.PatientID] = oldPatient;
                PrintPatients();
            }
            return true;
        }

        public ulong GetNewPatientID()
        {
            if (PatientCollection.Count > 0)
                return PatientCollection.Values.Max((p) => { return p.PatientID; }) + 1;
            return 1;
        }


        public IList<Patient> GetPatients()
        {
            return PatientCollection.Values.ToList();
        }

        private void PrintPatients()
        {
            foreach (var p in PatientCollection.Values)
            {
                Console.WriteLine("\t\t{0}.\t{1}\t{2}\t{3}", p.PatientID, p.PatientName, p.PatientAge, p.PatientSex);
            }
            Console.WriteLine();
        }
    }
}

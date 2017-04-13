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
using UIH.XR.XSample.BusinessLogicInterface;
using System.Collections.ObjectModel;

namespace UIH.XR.XSample.ViewModels
{
    [Export("PatientViewModel")]
    public class PatientViewModel:NotificationObject
    {
        [ImportingConstructor]
        public PatientViewModel([Import(typeof(IPatientSource))]IPatientSource patientSource)
        {
            _description = "Patient";
            PatientSource = patientSource;


            PatientItemSource = new ObservableCollection<Patient>();
            Update();

            NewCommand = new SampleCommand(DoNew);
            DeleteCommand = new SampleCommand(DoDelete);
            ModifyCommand = new SampleCommand(DoModify);
        }

        public IPatientSource PatientSource { get; set; }

        public ObservableCollection<Patient> PatientItemSource { get; set; }

        public SampleCommand NewCommand { get; set; }

        public SampleCommand DeleteCommand { get; set; }

        public SampleCommand ModifyCommand { get; set; }


        private void DoNew(object obj)
        {
            bool result=  PatientSource.NewPatient(PresentPatient.Copy());
            Console.WriteLine("DoNew:\t" + result);
            Update();
        }

        private void DoDelete(object obj)
        {
            bool result = PatientSource.DeletePatient(CurrentPatient.PatientID);
            Console.WriteLine("DoNew:\t" + result);
            Update();
        }

        private void DoModify(object obj)
        {
            bool result = PatientSource.ModifyPatient(PresentPatient);
            Console.WriteLine("DoNew:\t" + result);
            Update();
        }

        private void Update()
        {
            PatientItemSource.Clear();
            foreach (var p in PatientSource.GetPatients())
            {
                PatientItemSource.Add(p);
            }
        }

        Patient _currentPatient;
        public Patient CurrentPatient
        {
            get { return _currentPatient; }
            set
            {
                _currentPatient = value;
                if (_currentPatient != null)
                    PresentPatient = _currentPatient.Copy();
                else
                    PresentPatient = new Patient();
                RaisePropertyChanged(() => CurrentPatient);
            }
        }

        Patient _presentPatient = new Patient();
        public Patient PresentPatient
        {
            get { return _presentPatient; }
            set
            {
                _presentPatient = value;
                RaisePropertyChanged(() => PresentPatient);
            }
        }
    }
}

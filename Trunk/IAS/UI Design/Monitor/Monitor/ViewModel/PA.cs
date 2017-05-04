using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Monitor.ViewModel
{
    public class PA :BasicModel
    {
        private Visibility _patientListAdminVisibility = Visibility.Collapsed;
        public Visibility PatientListAdminVisibility
        {
            get { return _patientListAdminVisibility; }
            set
            {
                _patientListAdminVisibility = value;
                OnPropertyChanged("PatientListAdminVisibility");
            }
        }

        private Visibility _patientAdminVisibility = Visibility.Visible;
        public Visibility PatientAdminVisibility
        {
            get { return _patientAdminVisibility; }
            set
            {
                _patientAdminVisibility = value;
                OnPropertyChanged("PatientAdminVisibility");
            }
        }
    }
}

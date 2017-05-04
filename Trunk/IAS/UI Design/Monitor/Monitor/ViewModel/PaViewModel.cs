using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Monitor.ViewModel
{
   public class PaViewModel :BasicModel
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

       private void SwitchContentControl(object e)
       {
           if (PatientAdminVisibility == Visibility.Visible && PatientListAdminVisibility == Visibility.Collapsed)
           {
               PatientAdminVisibility = Visibility.Collapsed;
               PatientListAdminVisibility = Visibility.Visible;
           }
           else if (PatientListAdminVisibility == Visibility.Visible && PatientAdminVisibility == Visibility.Collapsed)
           {
               PatientAdminVisibility = Visibility.Visible;
               PatientListAdminVisibility = Visibility.Collapsed;
           }
          
       }
    }
}

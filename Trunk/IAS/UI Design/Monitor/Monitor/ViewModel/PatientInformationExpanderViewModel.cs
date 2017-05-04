using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor.ViewModel
{
    public class PatientInformationExpanderViewModel :BasicModel
    {

        public PatientInformationExpanderViewModel()
        {
            
        }

        private string _expanderName = "Information";
        public string ExpanderName
        {
            get { return _expanderName; }
            set
            {
                _expanderName = value;
                OnPropertyChanged("ExpanderName");
            }
        }

        private bool  _isExpanded = true;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged("IsExpanded");
                if (IsExpanded.Equals(false))
                {
                    ExpanderName = "Hide";
                }
                else
                {
                    ExpanderName = "Information";
                }
            }
        }
    }
}

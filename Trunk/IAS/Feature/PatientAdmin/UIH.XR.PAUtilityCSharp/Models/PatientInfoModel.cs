using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using UIH.XA.PAUtilityCSharp.Commons;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Models
{
    [Serializable]
    public class PatientInfoModel : ViewModelBase
    {
        //(0061,0001) MaxLength:64
        private string _patientUid = "";        
        public string PatientUid
        {
            get { return _patientUid; }
            set
            {
                if (value == _patientUid)
                {
                    return;
                }

                _patientUid = value;
                OnPropertyChanged("PatientUid");
            }
        }

        //(0010,0020) MaxLength:256
        private string _patientId = "";
        public string PatientId
        {
            get { return _patientId; }
            set
            {
                if (value == _patientId)
                {
                    return;
                }

                if (string.IsNullOrEmpty(value))
                {
                    _patientId = string.Empty;
                }
                else
                {
                    _patientId = value.Trim();
                }

                OnPropertyChanged("PatientId");
            }
        }

        //(0010,0021) MaxLength:256
        private string _issuerOfPatientId = "";
        public string IssuerOfPatientId
        {
            get { return _issuerOfPatientId; }
            set
            {
                if (value == _issuerOfPatientId)
                {
                    return;
                }
                _issuerOfPatientId = value;
                OnPropertyChanged("IssuerOfPatientId");
            }
        }

        //(0010,0010) MaxLength:64
        private string _patientName = "";
        public string PatientName
        {
            get { return _patientName; }
            set
            {
                if (value == _patientName)
                {
                    return;
                }

                _patientName = value;

                OnPropertyChanged("PatientName");
            }
        }

        /// <summary>
        /// Chinese Pinyin string of patient name
        /// </summary>
        //private string pYName = string.Empty;

        //public string PYName
        //{
        //    get
        //    {
        //        pYName = Utility.CHSPinYinConvert(PatientName);

        //        return pYName;
        //    }
        //    set { pYName = value; }
        //}

        //(0010,0030) DateTimeFormat:yyyy-mm-dd
        private DateTime? _patientBirthDate;

        public DateTime? PatientBirthDate
        {
            get { return _patientBirthDate; }
            set
            {
                if (value == _patientBirthDate)
                {
                    return;
                }

                _patientBirthDate = value;

                OnPropertyChanged("PatientBirthDate");
            }
        }

        public string CurrentCultureDateFormat
        {
            get { return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern; }
        }

        //(0010,0040) F/M
        private Gender? _patientSex = null;

        public Gender? PatientSex
        {
            get { return _patientSex; }
            set
            {
                if (value == _patientSex)
                {
                    return;
                }

                _patientSex = value;
                OnPropertyChanged("PatientSex");
            }
        }

        //(0010,4000) MaxLength:1024 
        private string _patientComments = "";

        public string PatientComments
        {
            get { return _patientComments; }
            set
            {
                if (value == _patientComments)
                {
                    return;
                }

                _patientComments = value;
                OnPropertyChanged("PatientComments");
            }
        }

        //(0010,2160) MaxLength:64
        private string _ethnicGroup = "";

        public string EthnicGroup
        {
            get { return _ethnicGroup; }
            set
            {
                if (value == _ethnicGroup)
                {
                    return;
                }

                _ethnicGroup = value;
                OnPropertyChanged("EthnicGroup");
            }
        }

        //(0010,1080) MaxLength:256
        private string _militaryRank = "";

        public string MilitaryRank
        {
            get { return _militaryRank; }
            set
            {
                if (value == _militaryRank)
                {
                    return;
                }

                _militaryRank = value;
                OnPropertyChanged("MilitaryRank");
            }
        }

       // 0-Accepted, 1-Un-accepted, 2-Rejected,3-Deleted
        private ConfirmStatus _patientConfirmStatus = ConfirmStatus.Accepted;

        public ConfirmStatus PatientConfirmStatus
        {
            get { return _patientConfirmStatus; }
            set
            {
                _patientConfirmStatus = value;
                OnPropertyChanged("PatientConfirmStatus");
            }
        }
        //MaxLength:64
        private string _voiceLanguage;

        public string VoiceLanguage
        {
            get { return _voiceLanguage; }
            set
            {
                _voiceLanguage = value;
                OnPropertyChanged("VoiceLanguage");
            }
        }
        //true :EmergencyPatient
        private bool isEmergency;

        public bool IsEmergency
        {
            get { return isEmergency; }
            set
            {
                if (value == isEmergency)
                {
                    return;
                }

                isEmergency = value;
                OnPropertyChanged("IsEmergency");
            }
        }
        //(0010,2110) 
        private string _allergies = string.Empty;

        public string Allergies
        {
            get { return _allergies; }
            set
            {
                _allergies = value;
                OnPropertyChanged("Allergies");
            }
        }

        private string _patientTel = "";

        public string PatientTel
        {
            get { return _patientTel; }
            set
            {
                _patientTel = value ?? string.Empty;
                OnPropertyChanged("PatientTel");
            }
        }

        private string _emergencyContact = "";

        public string EmergencyContact
        {
            get { return _emergencyContact; }
            set
            {
                _emergencyContact = value ?? string.Empty;
                OnPropertyChanged("EmergencyContact");
            }
        }

        private string _homeAddress = string.Empty;

        public string HomeAddress
        {
            get { return _homeAddress; }
            set
            {
                _homeAddress = value ?? string.Empty;
                OnPropertyChanged("HomeAddress");
            }
        }

        private string _companyAddress = string.Empty;

        public string CompanyAddress
        {
            get { return _companyAddress; }
            set
            {
                _companyAddress = value;
                OnPropertyChanged("CompanyAddress");
            }
        }
    }
}

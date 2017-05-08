using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UIH.XA.PAUtilityCSharp.Enumeration;

namespace UIH.XA.PAUtilityCSharp.Models
{
    [Serializable]
    public class StudyInfoModel : ViewModelBase,
                                  ICommonProperty,
                                  IArchive
    {
        private PatientInfoModel patient = new PatientInfoModel();

        public PatientInfoModel Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        private AgeWithUnit _patientAge = new AgeWithUnit
        {
            AgeUnit = AgeUnit.Year
        };

        public AgeWithUnit PatientAge
        {
            get
            {
                return _patientAge;
            }
            set
            {
                if (value == _patientAge)
                {
                    return;
                }

                _patientAge = value;
                OnPropertyChanged("PatientAge");
            }
        }

        //(0010,1020) Length or size of the Patient, in meters. DS
        private HeightWithUnit _patientSize;

        public HeightWithUnit PatientSize
        {
            get
            {
                return _patientSize;
            }
            set
            {
                if (value == _patientSize)
                {
                    return;
                }

                _patientSize = value;
                OnPropertyChanged("PatientSize");
            }
        }

        //(0010,1030) Patient Weight PatientWeight DS unit kg
        private WeightWithUnit _patientWeight;

        public WeightWithUnit PatientWeight
        {
            get
            {
                return _patientWeight;
            }
            set
            {
                if (value == _patientWeight)
                {
                    return;
                }

                _patientWeight = value;
                OnPropertyChanged("PatientWeight");
            }
        }

        private Protect _protect = Protect.Null;

        public Protect Protect
        {
            get
            {
                return _protect;
            }
            set
            {
                _protect = value;
                OnPropertyChanged("Protect");
            }
        }

        //(0020,000D) Study Instance UID StudyInstanceUID UI
        public string InstanceUID
        {
            get;
            set;
        }

        //(0020,0010) DICOM Study ID  user or equipment generated study identifier
        private string _studyId = "";

        public string StudyId
        {
            get
            {
                return _studyId;
            }
            set
            {
                _studyId = value ?? string.Empty;
                OnPropertyChanged("StudyId");
            }
        }

        //(0008,0020) Study Date StudyDate DA.   
        //A String of characters of the format:YYYYMMDD where YYYY shall contain year,            
        //Example:19930822 would represent August 2
        private DateTime? _studyDate;

        public DateTime? StudyDate
        {
            get
            {
                return _studyDate;
            }
            set
            {
                _studyDate = value;

                OnPropertyChanged("StudyDate");
            }
        }

        //(0008,0030) Study Time StudyTime TM.
        //A String of characters of the format:HHMMSS.FFFFFF;
        private TimeSpan? _studyTime;

        public TimeSpan? StudyTime
        {
            get
            {
                return _studyTime;
            }
            set
            {
                _studyTime = value;

                OnPropertyChanged("StudyTime");
            }
        }

        private DateTime? _studyDateTime;

        public DateTime? StudyDateTime
        {
            get
            {
                return _studyDateTime;
            }
            set
            {
                _studyDateTime = value;
                OnPropertyChanged("StudyDateTime");
            }
        }

        //(0008,0050) Accession Number AccessionNumber SH. 
        private string _accessionNumber = "";

        public string AccessionNumber
        {
            get
            {
                return _accessionNumber;
            }
            set
            {
                _accessionNumber = value ?? string.Empty;
                OnPropertyChanged("AccessionNumber");
            }
        }

        //(0008,1030) Study Description StudyDescription LO
        private string _studyDescription = "";

        public string StudyDescription
        {
            get
            {
                return _studyDescription;
            }
            set
            {
                _studyDescription = value ?? string.Empty;
                OnPropertyChanged("StudyDescription");
            }
        }

        private Source _source = Source.Localhost;

        public Source Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnPropertyChanged("Source");
            }
        }

        private Source? _importSource;

        public Source? ImportSource
        {
            get
            {
                return _importSource;
            }
            set
            {
                _importSource = value;
                OnPropertyChanged("ImportSource");
            }
        }

        private DateTime? _importDateTime = null;

        public DateTime? ImportDateTime
        {
            get
            {
                return _importDateTime;
            }
            set
            {
                _importDateTime = value;
                OnPropertyChanged("ImportDateTime");
            }
        }

        private string _patientInstitutionResidence = "";

        public string PatientInstitutionResidence
        {
            get
            {
                return _patientInstitutionResidence;
            }
            set
            {
                if (value == _patientInstitutionResidence)
                {
                    return;
                }

                _patientInstitutionResidence = value;
                OnPropertyChanged("PatientInstitutionResidence");
            }
        }

        private string _admissionId;

        public string AdmissionId
        {
            get
            {
                return _admissionId;
            }
            set
            {
                _admissionId = value;
                OnPropertyChanged("AdmissionId");
            }
        }

        private ExaminedStatus _studyFlag = ExaminedStatus.Scheduled;

        public ExaminedStatus StudyFlag
        {
            get
            {
                return _studyFlag;
            }
            set
            {
                _studyFlag = value;
                OnPropertyChanged("StudyFlag");
            }
        }

        private PrintStatus _printStatus = PrintStatus.UnPrinted;

        public PrintStatus PrintStatus
        {
            get
            {
                return _printStatus;
            }
            set
            {
                _printStatus = value;
                OnPropertyChanged("PrintStatus");
            }
        }

        private ConfirmStatus _studyConfirmStatus = ConfirmStatus.Accepted;

        public ConfirmStatus StudyConfirmStatus
        {
            get
            {
                return _studyConfirmStatus;
            }
            set
            {
                _studyConfirmStatus = value;
                OnPropertyChanged("StudyConfirmStatus");
            }
        }

        private string _studyComment = "";

        public string StudyComment
        {
            get
            {
                return _studyComment;
            }
            set
            {
                _studyComment = value;
                OnPropertyChanged("StudyComment");
            }
        }

        private bool _modifyFlag;

        public bool ModifyFlag
        {
            get
            {
                return _modifyFlag;
            }
            set
            {
                _modifyFlag = value;
                OnPropertyChanged("ModifyFlag");
            }
        }

        private bool _mergeFlag;

        public bool MergeFlag
        {
            get
            {
                return _mergeFlag;
            }
            set
            {
                _mergeFlag = value;
                OnPropertyChanged("MergeFlag");
            }
        }

        private bool _splitFlag;

        public bool SplitFlag
        {
            get
            {
                return _splitFlag;
            }
            set
            {
                _splitFlag = value;
                OnPropertyChanged("SplitFlag");
            }
        }

        private string _operatorName = string.Empty;

        public string OperatorName
        {
            get
            {
                return _operatorName;
            }
            set
            {
                _operatorName = value;
                OnPropertyChanged("OperatorName");
            }
        }

        private string _requestingPhysician = "";

        public string RequestingPhysician
        {
            get
            {
                return _requestingPhysician;
            }
            set
            {
                _requestingPhysician = value ?? string.Empty;
                OnPropertyChanged("RequestingPhysician");
            }
        }

        //(0008,0090) Referring Physician Name ReferringPhysicianName PN
        private string _referringPhysicianName = "";

        public string ReferringPhysicianName
        {
            get
            {
                return _referringPhysicianName;
            }
            set
            {
                _referringPhysicianName = value ?? string.Empty;
                OnPropertyChanged("ReferringPhysicianName");
            }
        }

        private string _performingPhysician;

        public string PerformingPhysician
        {
            get
            {
                return _performingPhysician;
            }
            set
            {
                _performingPhysician = value;
                OnPropertyChanged("PerformingPhysician");
            }
        }

        private string _requestedProcedureDescription = "";

        public string RequestedProcedureDescription
        {
            get
            {
                return _requestedProcedureDescription;
            }
            set
            {
                _requestedProcedureDescription = value ?? string.Empty;
                OnPropertyChanged("RequestedProcedureDescription");
            }
        }

        private string _procedureStepDescription;

        public string ProcedureStepDescription
        {
            get
            {
                return _procedureStepDescription;
            }
            set
            {
                _procedureStepDescription = value;
                OnPropertyChanged("ProcedureStepDescription");
            }
        }

        private string _procedureStepId;

        public string ProcedureStepId
        {
            get
            {
                return _procedureStepId;
            }
            set
            {
                _procedureStepId = value;
                OnPropertyChanged("ProcedureStepId");
            }
        }

        private DateTime? _risDate;

        public DateTime? RisDate
        {
            get
            {
                return _risDate;
            }
            set
            {
                _risDate = value;
                OnPropertyChanged("RisDate");
            }
        }

        private string _bodyPartExamed = string.Empty;

        public string BodyPartExamed
        {
            get
            {
                return _bodyPartExamed;
            }
            set
            {
                _bodyPartExamed = value;
                OnPropertyChanged("BodyPartExamed");
            }
        }

        private string _archivedPacsNodes = string.Empty;

        public string ArchivedPacsNodes
        {
            get
            {
                if (_archivedPacsNodes != null)
                {
                    return _archivedPacsNodes.Contains(",")
                               ? _archivedPacsNodes.Replace(",", ", ")
                               : _archivedPacsNodes;
                }
                return _archivedPacsNodes;
            }
            set
            {
                _archivedPacsNodes = value;
                OnPropertyChanged("ArchivedPacsNodes");
            }
        }

        private string _modifier = string.Empty;

        public string Modifier
        {
            get
            {
                return _modifier;
            }
            set
            {
                _modifier = value;
                OnPropertyChanged("Modifier");
            }
        }

        public string RequestedProcedureId
        {
            get;
            set;
        }

        public static bool NeedReloadPrConfig
        {
            get;
            set;
        }

        public bool IsArchived
        {
            get
            {
                return SendStatus == SendStatus.PartlySent || SendStatus == SendStatus.TotalSent
                       || StoredInDVd == StoredStatus.PartlyStored || StoredInDVd == StoredStatus.Stored
                       || StoredInUsb == StoredStatus.PartlyStored || StoredInUsb == StoredStatus.Stored;
            }
        }

        private SendStatus _sendStatus = SendStatus.UnSent;

        public SendStatus SendStatus
        {
            get
            {
                return _sendStatus;
            }
            set
            {
                _sendStatus = value;
                OnPropertyChanged("SendStatus");
                OnPropertyChanged("TotalArchivingStatus");
            }
        }

        private StoredStatus _storedInDVd = StoredStatus.UnStored;

        public StoredStatus StoredInDVd
        {
            get
            {
                return _storedInDVd;
            }
            set
            {
                _storedInDVd = value;
                OnPropertyChanged("StoredInDVd");
                OnPropertyChanged("TotalArchivingStatus");
            }
        }

        private StoredStatus _storedInUsb = StoredStatus.UnStored;

        public StoredStatus StoredInUsb
        {
            get
            {
                return _storedInUsb;
            }
            set
            {
                _storedInUsb = value;
                OnPropertyChanged("StoredInUsb");
                OnPropertyChanged("TotalArchivingStatus");
            }
        }

        public StoredStatus TotalArchivingStatus
        {
            get
            {
                if (SendStatus == SendStatus.TotalSent
                    || StoredInDVd == StoredStatus.Stored
                    || StoredInUsb == StoredStatus.Stored)
                {
                    return StoredStatus.Stored;
                }
                if (SendStatus == SendStatus.PartlySent
                    || StoredInDVd == StoredStatus.PartlyStored
                    || StoredInUsb == StoredStatus.PartlyStored)
                {
                    return StoredStatus.PartlyStored;
                }
                return StoredStatus.UnStored;
            }
        }

        public void SetToUnarchived()
        {
            SendStatus = SendStatus.UnSent;
            StoredInDVd = StoredStatus.UnStored;
            StoredInUsb = StoredStatus.UnStored;
        }

        private ObservableCollection<SeriesInfoModel> _seriesList = new ObservableCollection<SeriesInfoModel>();

        public ObservableCollection<SeriesInfoModel> SeriesList
        {
            get { return _seriesList; }
            set
            {
                _seriesList = value;
                OnPropertyChanged("SeriesList");
            }
        }

        public override string ToString()
        {
            return String.Format("{0} ID:{1} InstanceUID:{2}", base.ToString(), StudyId, InstanceUID);
        }
    }
}

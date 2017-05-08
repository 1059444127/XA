using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using UIH.XA.PAUtilityCSharp.Commands;
using UIH.XA.PAUtilityCSharp.Commons;
using UIH.XA.PAUtilityCSharp.Database;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Events;
using UIH.XA.PAUtilityCSharp.Models;
using UIH.XA.PAUtilityCSharp.Models.ConfigRelate;

namespace UIH.XA.PatientAdmin.ViewModel
{
    [Export("PRViewModel")]
    public class PRViewModel: ViewModelBase
    {
        DataAccessHelper _dataAccesser = DataAccessHelper.Default;

        IEventAggregator eventAggregator;
        SubscriptionToken token;
        [XmlIgnoreAttribute]
        public IDGeneratorParam IdCreater { get; private set; }

        [XmlIgnoreAttribute]
        public UIDCreatManager UidCreatManager
        {
            get;
            private set;
        }

        public string CurOperatorName
        {
            get;
            set;
        }

        #region Property

        private StudyInfoModel _studyInfo = null;       
        public StudyInfoModel StudyInfo
        {
            get { return _studyInfo; }
            set
            {
                _studyInfo = value;
                OnPropertyChanged("StudyInfo");
            }
        }

        private StudyInfoModel _studyInfoCopy = null;
        public StudyInfoModel StudyInfoCopy
        {
            get { return _studyInfoCopy; }
            set
            {
                _studyInfoCopy = value;
                OnPropertyChanged("StudyInfoCopy");
            }
        }

        private UserDataPhysicianOption physicianOptionConfig;

        private ObservableCollection<string> _operatorNames;
        public ObservableCollection<string> OperatorNames
        {
            get { return _operatorNames; }
            set
            {
                _operatorNames = value;
                OnPropertyChanged("OperatorNames");
            }
        }

        private ObservableCollection<string> _performingPhysicianNames;
        public ObservableCollection<string> PerformingPhysicianNames
        {
            get { return _performingPhysicianNames; }
            set
            {
                _performingPhysicianNames = value;
                OnPropertyChanged("PerformingPhysicianNames");
            }
        }

        private ObservableCollection<string> _referencePhysicianNames;
        public ObservableCollection<string> ReferencePhysicianNames
        {
            get { return _referencePhysicianNames; }
            set
            {
                _referencePhysicianNames = value;
                OnPropertyChanged("ReferencePhysicianNames");
            }
        }

        private ObservableCollection<string> _requestingPhysicianNames;
        public ObservableCollection<string> RequestingPhysicianNames
        {
            get { return _requestingPhysicianNames; }
            set
            {
                _requestingPhysicianNames = value;
                OnPropertyChanged("RequestingPhysicianNames");
            }
        }

        private StudyOperateFlag _operateFlag;
        public StudyOperateFlag OperateFlag
        {
            get { return _operateFlag; }
            set
            {
                _operateFlag = value;
                OnPropertyChanged("OperateFlag");
            }
        }

        private bool _isEmergency = false;        
        public bool IsEmergency
        {
            get { return _isEmergency; }
            set
            {
                _isEmergency = value;
                OnPropertyChanged("IsEmergency");
            }
        }
        #endregion

        #region Command
        private DelegateCommand _examCommand;

        public DelegateCommand ExamCommand
        {
            get { return _examCommand ?? (_examCommand = new DelegateCommand(ExamCommandHandler)); }
        }

        private DelegateCommand _savePreRegCommand;

        public DelegateCommand SavePreRegCommand
        {
            get
            {
                if (null == _savePreRegCommand)
                {
                    _savePreRegCommand = new DelegateCommand(SavePreRegCommandHandler);
                }
                return _savePreRegCommand;
            }
        }
        #endregion

        #region CommandHandler
        private void ExamCommandHandler()
        {
            
          //save and send param to appmanger
           
        }

        private void SavePreRegCommandHandler()
        {
            //save to db
        }

        #endregion

        public PRViewModel()
        {
            InitIdCreater();

            var studyinfo = new StudyInfoModel();
            var patient = new PatientInfoModel();
            studyinfo.AccessionNumber = "asdasd";
            patient.PatientName = "juli.hu";
            studyinfo.Patient = patient;
            StudyInfo = studyinfo;

            //调用此方法为了提前初始化jason类中的数据，以便该方法在第2次调用后速度更快
            //该方法在第一次调用时很慢
            SerializeHelper.CloneObject<StudyInfoModel>(new StudyInfoModel());

            //订阅事件
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAggregator.GetEvent<PaSelectChangeEvent>().Subscribe(PrStudyInfoChange);

        }

        //事件触发
        public void PrStudyInfoChange(StudyInfoModel selectStudyInfoModel)
        {
            this.StudyInfo = selectStudyInfoModel;
        }

        private void InitIdCreater()
        {
            
            this.IdCreater = new IDGeneratorParam();
            this.IdCreater.Load();
            UidCreatManager=new UIDCreatManager();
        }
    }
}

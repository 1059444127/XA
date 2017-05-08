using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using UIH.XA.PAUtilityCSharp.Commands;
using UIH.XA.PAUtilityCSharp.Commons.TaskSchedulerExtension;
using UIH.XA.PAUtilityCSharp.Events;
using UIH.XA.PAUtilityCSharp.Models;

namespace UIH.XR.PatientAdmin.ViewModel
{
    [Export("PAViewModel")]
    public class PAViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;

        private readonly TaskFactory _taskFactoryDataQuery =
            new TaskFactory(new TaskSchedulerBase<DynamicDataQueryQueueScheduler>());
        public PAViewModel()
        {
            this.eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        #region Property

        private ObservableCollection<StudyInfoModel> _uiStudyList;

        public ObservableCollection<StudyInfoModel>UiStudyList
        {
            get
            {
                return _uiStudyList;
            }
            set
            {
                _uiStudyList = value;
                OnPropertyChanged("UiStudyList");
            }
        }
        private ObservableCollection<StudyInfoModel> _checkStudyList;

        public ObservableCollection<StudyInfoModel> CheckStudyList
        {
            get
            {
                return _checkStudyList;
            }
            set
            {
                _checkStudyList = value;
                OnPropertyChanged("CheckStudyList");
            }
        }

        private ObservableCollection<StudyInfoModel> _unCheckStudyList;

        public ObservableCollection<StudyInfoModel> UnCheckStudyList
        {
            get
            {
                return _unCheckStudyList;
            }
            set
            {
                _unCheckStudyList = value;
                OnPropertyChanged("UnCheckStudyList");
            }
        }

        private ObservableCollection<StudyInfoModel> _allStudyList;

        public ObservableCollection<StudyInfoModel> AllStudyList
        {
            get
            {
                return _allStudyList;
            }
            set
            {
                _allStudyList = value;
                OnPropertyChanged("AllStudyList");
            }
        }

        public List<StudyInfoModel> SelectedStudyItems
        {
            get;
            private set;
        }

        private ObservableCollection<StudyInfoModel> _selectedStudyCollection = null;

        public ObservableCollection<StudyInfoModel> SelectedStudyCollection
        {
            get
            {
                return _selectedStudyCollection;
            }
            set
            {
                _selectedStudyCollection = value;
                OnPropertyChanged("SelectedStudyCollection");
            }
        }

        private StudyInfoModel _selectedStudyItem;

        public StudyInfoModel SelectedStudyItem
        {
            get
            {
                return _selectedStudyItem;
            }
            set
            {
                _selectedStudyItem = value;
                OnPropertyChanged("SelectedStudyItem");
            }
        }

        #endregion


        #region Command

        DelegateCommand _sendStudyInfoToPr;

        DelegateCommand SendStudyInfoToPr
        {
            get
            {
                return _sendStudyInfoToPr ?? ( _sendStudyInfoToPr = new DelegateCommand(SendStudyInfoToPrHandler) );
            }
        }

        DelegateCommand _addNewPatient;

        DelegateCommand AddNewPatient
        {
            get
            {
                return _addNewPatient ?? ( _addNewPatient = new DelegateCommand(AddNewPatientHandler) );
            }
        }

        DelegateCommand _paExamCommand;

        DelegateCommand PaExamCommand
        {
            get
            {
                return _paExamCommand ?? ( _paExamCommand = new DelegateCommand(PaExamCommandHandler) );
            }
        }

        DelegateCommand _paEmergencyCommand;

        DelegateCommand PaEmergencyCommand
        {
            get
            {
                return _paEmergencyCommand ?? ( _paEmergencyCommand = new DelegateCommand(PaEmergencyCommandHandler) );
            }
        }

        DelegateCommand _paModifyCommand;

        DelegateCommand PaModifyCommand
        {
            get
            {
                return _paModifyCommand ?? (_paModifyCommand = new DelegateCommand(PaModifyCommandHandler));
            }
        }

        DelegateCommand _paMergeCommand;

        DelegateCommand PaMergeCommand
        {
            get
            {
                return _paMergeCommand ?? ( _paMergeCommand = new DelegateCommand(PaMergeCommandHandler) );
            }
        }

        DelegateCommand _paSplitCommand;

        DelegateCommand PaSplitCommand
        {
            get
            {
                return _paSplitCommand ?? (_paSplitCommand = new DelegateCommand(PaSplitCommandHandler));
            }
        }

        DelegateCommand _paProtectCommand;

        DelegateCommand PaProtectCommand
        {
            get
            {
                return _paProtectCommand ?? (_paProtectCommand = new DelegateCommand(PaProtectCommandHandler));
            }
        }

        DelegateCommand _paDeleteCommand;

        DelegateCommand PaDeleteCommand
        {
            get
            {
                return _paDeleteCommand ?? (_paDeleteCommand = new DelegateCommand(PaDeleteCommandHandler));
            }
        }

        DelegateCommand _paSearchCommand;

        DelegateCommand PaSearchCommand
        {
            get
            {
                return _paSearchCommand ?? (_paSearchCommand = new DelegateCommand(PaSearchCommandHandler));
            }
        }

        #endregion


        #region CommandHandler

        private void SendStudyInfoToPrHandler()
        {
            eventAggregator.GetEvent<PaSelectChangeEvent>().Publish(SelectedStudyItem);
        }

        private void AddNewPatientHandler()
        {
            eventAggregator.GetEvent<AddNewPatientEvent>().Publish(null);
        }

        private void PaExamCommandHandler()
        {
            //enter exam
        }

        private void PaEmergencyCommandHandler()
        {
        }

        private void PaModifyCommandHandler()
        {
           
        }

        private void PaMergeCommandHandler()
        {
            
        }

        private void PaSplitCommandHandler()
        {
            throw new NotImplementedException();
        }

        private void PaProtectCommandHandler()
        {
            throw new NotImplementedException();
        }

        private void PaDeleteCommandHandler()
        {
            throw new NotImplementedException();
        }

        private void PaSearchCommandHandler()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

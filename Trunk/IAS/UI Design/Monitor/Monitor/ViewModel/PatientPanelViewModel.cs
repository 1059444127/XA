using System.Collections.ObjectModel;
using System.Windows;

namespace Monitor.ViewModel
{
   public class PatientPanelViewModel :BasicModel
    {

       public ObservableCollection<StudyModel> PatientList { get; set; }

       public ObservableCollection<ComBoxItem> ComBoxItems { get; set; }

       private Visibility _visibility = Visibility.Visible;
       public Visibility Visibility
       {
           get { return _visibility; }
           set
           {
               _visibility = value;
               OnPropertyChanged("Visibility");
           }
       }

       public PatientPanelViewModel()
       {
           PatientList = new ObservableCollection<StudyModel>();
           var studyModel = new StudyModel();
           foreach (var item in studyModel.GetItems())
           {
               studyModel.StudyList.Add(item);
           }
           studyModel.Name = "A";
           studyModel.Image = "11";
           studyModel.Age = 12;
           studyModel.StudyId = "111";
           studyModel.Data = "2017.05.03";
           studyModel.LockedImage = "**";
           studyModel.SavedImage = "**";
           studyModel.PrintedImage = "**";
           PatientList.Add(studyModel);

           studyModel = new StudyModel();
           studyModel.Name = "B";
           studyModel.Image = "11";
           studyModel.Age = 12;
           studyModel.StudyId = "111";
           studyModel.Data = "2017.05.03";
           studyModel.LockedImage = "**";
           studyModel.SavedImage = "**";
           studyModel.PrintedImage = "**";
           PatientList.Add(studyModel);

           studyModel = new StudyModel();
           foreach (var item in studyModel.GetItems())
           {
               studyModel.StudyList.Add(item);
           }
           studyModel.Name = "C";
           studyModel.Image = "11";
           studyModel.Age = 12;
           studyModel.StudyId = "111";
           studyModel.Data = "2017.05.03";
           studyModel.LockedImage = "**";
           studyModel.SavedImage = "**";
           studyModel.PrintedImage = "**";
           PatientList.Add(studyModel);

           studyModel = new StudyModel();
           studyModel.Name = "D";
           studyModel.Image = "11";
           studyModel.Age = 12;
           studyModel.StudyId = "111";
           studyModel.Data = "2017.05.03";
           studyModel.LockedImage = "**";
           studyModel.SavedImage = "**";
           studyModel.PrintedImage = "**";
           PatientList.Add(studyModel);

           studyModel = new StudyModel();
           foreach (var item in studyModel.GetItems())
           {
               studyModel.StudyList.Add(item);
           }
           studyModel.Name = "E";
           studyModel.Image = "11";
           studyModel.Age = 12;
           studyModel.StudyId = "111";
           studyModel.Data = "2017.05.03";
           studyModel.LockedImage = "**";
           studyModel.SavedImage = "**";
           studyModel.PrintedImage = "**";
           PatientList.Add(studyModel);

           studyModel = new StudyModel();
           studyModel.Name = "F";
           studyModel.Image = "11";
           studyModel.Age = 12;
           studyModel.StudyId = "111";
           studyModel.Data = "2017.05.03";
           studyModel.LockedImage = "**";
           studyModel.SavedImage = "**";
           studyModel.PrintedImage = "**";
           PatientList.Add(studyModel);

           ComBoxItems = new ObservableCollection<ComBoxItem>();
           var localItem = new ComBoxItem();
           localItem.Name = "Local";
           ComBoxItems.Add(localItem);

           var risItem = new ComBoxItem();
           risItem.Name = "Ris";
           ComBoxItems.Add(risItem);
       }
    }
}

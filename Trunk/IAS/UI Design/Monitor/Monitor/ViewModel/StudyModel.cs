using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Monitor.ViewModel
{
    public class StudyModel :BasicModel
    {
        public ObservableCollection<StudyItem> StudyList { get; set; }

        public StudyModel()
        {
            StudyList = new ObservableCollection<StudyItem>();
        }

        public IEnumerable<StudyItem> GetItems()
        {
            var items = new Collection<StudyItem>();

            var itemA= new StudyItem();
            itemA.ItemId = "1";
            itemA.ItemData = "2017";
            itemA.LockedImage = "**";
            itemA.PrintedImage = "**";
            itemA.SavedImage = "**";
            items.Add(itemA);

            var itemB = new StudyItem();
            itemB.ItemId = "2";
            itemB.ItemData = "2017";
            itemB.LockedImage = "**";
            itemB.PrintedImage = "**";
            itemB.SavedImage = "**";
            items.Add(itemB);

            return items;
        }

        public bool ContainItem { get; set; }

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _image = string.Empty;
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        private int _age;
        public int Age
        {
            get { return -_age; }
            set
            {
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        private string _studyId;
        public string StudyId
        {
            get { return _studyId; }
            set
            {
                _studyId = value;
                OnPropertyChanged("StudyId");
            }
        }

        private string _data;
        public string Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private string _lockedImage;
        public string LockedImage
        {
            get { return _lockedImage; }
            set
            {
                _lockedImage = value;
                OnPropertyChanged("LockedImage");
            }
        }

        private string _printedImage;
        public string PrintedImage
        {
            get { return _printedImage; }
            set
            {
                _printedImage = value;
                OnPropertyChanged("PrintedImage");
            }
        }

        private string _savedImage;
        public string SavedImage
        {
            get { return _savedImage; }
            set
            {
                _savedImage = value;
                OnPropertyChanged("SavedImage");
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

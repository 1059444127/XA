using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Monitor.ViewModel;

namespace Monitor.windows
{
    public class ItemImage : BasicModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }


        private string _studyID;
        public string StudyID
        {
            get { return _studyID; }
            set
            {
                _studyID = value;
                OnPropertyChanged("StudyID");
            }
        }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged("Visibility");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Monitor.Review
{
    public class ContentViewModel :BasicModel
    {
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
    }
}

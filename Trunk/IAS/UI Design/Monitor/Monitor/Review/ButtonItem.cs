using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor.Review
{
    public class ButtonItem :BasicModel,IDataItem
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

        public string GetItemType()
        {
            return "Button";
        }
    }
}

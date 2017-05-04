using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor.ViewModel
{
   public class StudyItem :BasicModel
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

       private string _itemId;
       public string ItemId
       {
           get { return _itemId; }
           set
           {
               _itemId = value;
               OnPropertyChanged("ItemId");
           }
       }

       private string _itemData;
       public string ItemData
       {
           get { return _itemData; }
           set
           {
               _itemData = value;
               OnPropertyChanged("ItemData");
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

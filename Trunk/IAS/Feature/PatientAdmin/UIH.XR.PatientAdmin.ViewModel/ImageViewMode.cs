using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using UIH.XA.PAUtilityCSharp.Models;

namespace UIH.XA.PatientAdmin.ViewModel
{
    [Export("ImageViewMode")]
    public class ImageViewMode:ViewModelBase
    {
         private ObservableCollection<ImageInfoModel> _imagesCollection = null;

        public ObservableCollection<ImageInfoModel> ImagesCollection
        {
            get { return _imagesCollection; }
            set
            {
                _imagesCollection = value;
                OnPropertyChanged("ImagesCollection");
            }
        }
    }
}

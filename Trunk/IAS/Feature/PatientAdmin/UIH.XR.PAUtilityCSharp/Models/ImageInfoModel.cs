using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.XA.PAUtilityCSharp.Models
{
    public class ImageInfoModel : ViewModelBase
    {
        private string thumbnailFilePath = "";

        public string ThumbnailFilePath
        {
            get
            {
                return thumbnailFilePath;
            }
            set
            {
                if (value == thumbnailFilePath)
                {
                    return;
                }

                thumbnailFilePath = value;
                OnPropertyChanged("ThumbnailFilePath");
            }
        }

        private string instanceUID = "";

        public string InstanceUID
        {
            get { return instanceUID; }
            set
            {
                if (value == instanceUID)
                {
                    return;
                }

                instanceUID = value;
                OnPropertyChanged("InstanceUID");
            }
        }

        private string parentInstanceUID;

        public string ParentInstanceUid
        {
            get { return this.parentInstanceUID; }
            set
            {
                this.parentInstanceUID = value;
                OnPropertyChanged("ParentInstanceUid");
            }
        }
    }
}

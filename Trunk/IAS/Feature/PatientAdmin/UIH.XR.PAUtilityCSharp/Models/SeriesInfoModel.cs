using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XA.PAUtilityCSharp.Enumeration;

namespace UIH.XA.PAUtilityCSharp.Models
{
    [Serializable]
    public class SeriesInfoModel : ViewModelBase, ICommonProperty, IArchive
    {
        private int _imageNumber;

        public int ImageNumber
        {
            get { return _imageNumber; }
            set
            {
                if (value == _imageNumber)
                {
                    return;
                }
                _imageNumber = value;
                OnPropertyChanged("ImageNumber");
            }
        }

        private Protect _protect = Protect.Null;

        public Protect Protect
        {
            get { return _protect; }
            set
            {
                _protect = value;
                OnPropertyChanged("Protect");
            }
        }

        private string _instanceUID = "";

        public string InstanceUID
        {
            get { return _instanceUID; }
            set
            {
                if (value == _instanceUID)
                {
                    return;
                }

                _instanceUID = value;
                OnPropertyChanged("InstanceUID");
            }
        }

        private string _studyInstanceUidFk = "";

        public string StudyInstanceUidFk
        {
            get { return _studyInstanceUidFk; }
            set
            {
                if (value == _studyInstanceUidFk)
                {
                    return;
                }

                _studyInstanceUidFk = value;
                OnPropertyChanged("StudyInstanceUidFk");
            }
        }

        private int _number;

        public int Number
        {
            get { return _number; }
            set
            {
                if (value == _number)
                {
                    return;
                }

                _number = value;
                OnPropertyChanged("Number");
            }
        }

        private string _description = "";

        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description)
                {
                    return;
                }

                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public bool IsArchived
        {
            get
            {
                return SendStatus == SendStatus.PartlySent ||
                       SendStatus == SendStatus.TotalSent ||
                       StoredInDvd == StoredStatus.PartlyStored ||
                       StoredInDvd == StoredStatus.Stored ||
                       StoredInUsb == StoredStatus.PartlyStored ||
                       StoredInUsb == StoredStatus.Stored;
            }
        }

        private SendStatus _sendStatus = SendStatus.UnSent;

        public SendStatus SendStatus
        {
            get { return _sendStatus; }
            set
            {
                if (value == _sendStatus)
                {
                    return;
                }

                _sendStatus = value;
                OnPropertyChanged("SendStatus");
                OnPropertyChanged("TotalArchivingStatus");
            }
        }

        private StoredStatus _storedInDvd = StoredStatus.UnStored;

        public StoredStatus StoredInDvd
        {
            get { return _storedInDvd; }
            set
            {
                if (value == _storedInDvd)
                {
                    return;
                }

                _storedInDvd = value;
                OnPropertyChanged("StoredInDvd");
                OnPropertyChanged("TotalArchivingStatus");
            }
        }

        private StoredStatus _storedInUsb = StoredStatus.UnStored;

        public StoredStatus StoredInUsb
        {
            get { return _storedInUsb; }
            set
            {
                if (value == _storedInUsb)
                {
                    return;
                }

                _storedInUsb = value;
                OnPropertyChanged("StoredInUsb");
                OnPropertyChanged("TotalArchivingStatus");
            }
        }

        public StoredStatus TotalArchivingStatus
        {
            get
            {
                if (SendStatus == SendStatus.TotalSent || StoredInDvd == StoredStatus.Stored ||
                    StoredInUsb == StoredStatus.Stored)
                    return StoredStatus.Stored;
                if (SendStatus == SendStatus.PartlySent || StoredInDvd == StoredStatus.PartlyStored ||
                    StoredInUsb == StoredStatus.PartlyStored)
                    return StoredStatus.PartlyStored;
                return StoredStatus.UnStored;
            }
        }

        private PrintStatus _printStatus = PrintStatus.UnPrinted;

        public PrintStatus PrintStatus
        {
            get { return _printStatus; }
            set
            {
                _printStatus = value;
                OnPropertyChanged("PrintStatus");
            }
        }

        private DateTime? _confirmDateTime = null;

        public DateTime? ConfirmDateTime
        {
            get { return _confirmDateTime; }
            set
            {
                _confirmDateTime = value;
                OnPropertyChanged("ConfirmDateTime");
            }
        }
    }
}

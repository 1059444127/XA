using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Monitor.PA;
using Monitor.windows;

namespace Monitor.ViewModel
{
    public class ImageBroswerViewModel:BasicModel
    {
        public ObservableCollection<ItemImage> ReferenceImageList { get; set; }

        public ObservableCollection<ItemImage> AllImageList { get; set; }

        private Visibility _visibility;
        public Visibility GroupBoxVisibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged("GroupBoxVisibility");
            }
        }

        public ImageBroswerViewModel()
        {
            ReferenceImageList = new ObservableCollection<ItemImage>();
            AllImageList = new ObservableCollection<ItemImage>();
            GroupBoxVisibility = Visibility.Visible;
            var referenceImageA = new ItemImage();
            referenceImageA.Visibility = Visibility.Visible;
            referenceImageA.Name = "A";
            referenceImageA.StudyID = "2017";
            referenceImageA.Type = "DR";
            ReferenceImageList.Add(referenceImageA);

            var referenceImageB = new ItemImage();
            referenceImageB.Visibility = Visibility.Visible;
            referenceImageB.Name = "B";
            referenceImageB.StudyID = "2017";
            referenceImageB.Type = "DR";
            ReferenceImageList.Add(referenceImageB);


            var allImageA = new ItemImage();
            allImageA.Visibility = Visibility.Collapsed;
            allImageA.Name = "A";
            allImageA.StudyID = "2017";
            allImageA.Type = "DR";
            AllImageList.Add(allImageA);

            var allImageB = new ItemImage();
            allImageB.Visibility = Visibility.Collapsed;
            allImageB.Name = "B";
            allImageB.StudyID = "2017";
            allImageB.Type = "DR";
            AllImageList.Add(allImageB);

            var allImageC = new ItemImage();
            allImageC.Visibility = Visibility.Collapsed;
            allImageC.Name = "C";
            allImageC.StudyID = "2017";
            allImageC.Type = "DR";
            AllImageList.Add(allImageC);

            var allImageD = new ItemImage();
            allImageD.Visibility = Visibility.Visible;
            allImageD.Name = "D";
            allImageD.StudyID = "2017";
            allImageD.Type = "DR";
            AllImageList.Add(allImageD);

            var allImageE = new ItemImage();
            allImageE.Visibility = Visibility.Collapsed;
            allImageE.Name = "A";
            allImageE.StudyID = "2017";
            allImageE.Type = "DR";
            AllImageList.Add(allImageE);

            var allImageF = new ItemImage();
            allImageF.Visibility = Visibility.Collapsed;
            allImageF.Name = "B";
            allImageF.StudyID = "2017";
            allImageF.Type = "DR";
            AllImageList.Add(allImageF);

            var allImageG = new ItemImage();
            allImageG.Visibility = Visibility.Collapsed;
            allImageG.Name = "C";
            allImageG.StudyID = "2017";
            allImageG.Type = "DR";
            AllImageList.Add(allImageG);

            var allImageH = new ItemImage();
            allImageH.Visibility = Visibility.Visible;
            allImageH.Name = "D";
            allImageH.StudyID = "2017";
            allImageH.Type = "DR";
            AllImageList.Add(allImageH);
            
            var allImageL = new ItemImage();
            allImageL.Visibility = Visibility.Collapsed;
            allImageL.Name = "B";
            allImageL.StudyID = "2017";
            allImageL.Type = "DR";
            AllImageList.Add(allImageL);

            var allImageM = new ItemImage();
            allImageM.Visibility = Visibility.Collapsed;
            allImageM.Name = "C";
            allImageM.StudyID = "2017";
            allImageM.Type = "DR";
            AllImageList.Add(allImageM);

            var allImageN = new ItemImage();
            allImageN.Visibility = Visibility.Visible;
            allImageN.Name = "D";
            allImageN.StudyID = "2017";
            allImageN.Type = "DR";
            AllImageList.Add(allImageN);

        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Monitor.PA;

namespace Monitor.ViewModel
{
    public class PatientAdminViewModel :BasicModel
    {

        public ObservableCollection<DataTemplateItem> ListItems { get; set; }

        public PatientAdminViewModel()
        {
            ListItems = new ObservableCollection<DataTemplateItem>();

            var itemA = new AddItem();
            ListItems.Add(itemA);

            var itemB = new ImageItem();
            itemB.Visibility = Visibility.Visible;
            itemB.Name = "B";
            itemB.StudyID = "2017";
            itemB.Type = "DR";
            ListItems.Add(itemB);

            var itemC = new ImageItem();
            itemC.Visibility = Visibility.Collapsed;
            itemC.Name = "C";
            itemC.StudyID = "2017";
            itemC.Type = "DR";
            ListItems.Add(itemC);

            var itemD = new ImageItem();
            itemD.Visibility = Visibility.Visible;
            itemD.Name = "D";
            itemD.StudyID = "2017";
            itemD.Type = "DR";
            ListItems.Add(itemD);

            var itemE = new ImageItem();
            itemE.Visibility = Visibility.Visible;
            itemE.Name = "E";
            itemE.StudyID = "2017";
            itemE.Type = "DR";
            ListItems.Add(itemE);

            var itemF = new ImageItem();
            itemF.Visibility = Visibility.Visible;
            itemF.Name = "F";
            itemF.StudyID = "2017";
            itemF.Type = "DR";
            ListItems.Add(itemF);
        }
    }
}

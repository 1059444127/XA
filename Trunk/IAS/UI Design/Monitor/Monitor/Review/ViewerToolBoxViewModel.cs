using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Monitor.Review
{
    public class ViewerToolBoxViewModel :BasicModel
    {

        public ViewerToolBoxViewModel()
        {
            ViewerToolBoxItemList = new ObservableCollection<ContentViewModel>();

            //Add Button
            //var itemA = new ItemViewModel();
            //itemA.Name = "Button1";
            //ViewerToolBoxItemList.Add(itemA);

            //var itemB = new ItemViewModel();
            //itemB.Name = "Button2";
            //ViewerToolBoxItemList.Add(itemB);

            //var itemC = new ItemViewModel();
            //itemC.Name = "Button3";
            //ViewerToolBoxItemList.Add(itemC);


            //Add TextBlock
            var itemA = new ContentViewModel();
            itemA.Name = "Text1";
            ViewerToolBoxItemList.Add(itemA);

            var itemB = new ContentViewModel();
            itemB.Name = "Text2";
            ViewerToolBoxItemList.Add(itemB);

            var itemC = new ContentViewModel();
            itemC.Name = "Text3";
            ViewerToolBoxItemList.Add(itemC);
        }

        private int _uniformGridColumn = 3;
        public int UniformGridColumn
        {
            get { return _uniformGridColumn; }
            set
            {
                _uniformGridColumn = value;
                OnPropertyChanged("UniformGridColumn");
            }
        }
        public ObservableCollection<ContentViewModel> ViewerToolBoxItemList { get; set; }


    }
}

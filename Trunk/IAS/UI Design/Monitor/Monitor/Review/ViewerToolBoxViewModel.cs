using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monitor.Review
{
    public class ViewerToolBoxViewModel :BasicModel
    {
        private int _uniformGridColumn = 0;
        public int UniformGridColumn
        {
            get { return _uniformGridColumn; }
            set
            {
                _uniformGridColumn = value;
                OnPropertyChanged("UniformGridColumn");
            }
        }

        private int _uniformGridRow = 0;
        public int UniformGridRow
        {
            get { return _uniformGridRow; }
            set
            {
                _uniformGridRow = value;
                OnPropertyChanged("UniformGridRow");
            }
        }

        private string _headerName = string.Empty;
        public string HeaderName
        {
            get { return _headerName; }
            set
            {
                _headerName = value;
                OnPropertyChanged("HeaderName");
            }
        }

        private int _toolsGridColumn = 0;
        public int ToolsGridColumn
        {
            get { return _toolsGridColumn; }
            set
            {
                _toolsGridColumn = value;
                OnPropertyChanged("ToolsGridColumn");
            }
        }

        private int _toolsGridRow = 0;
        public int ToolsGridRow
        {
            get { return _toolsGridRow; }
            set
            {
                _toolsGridRow = value;
                OnPropertyChanged("ToolsGridRow");
            }
        }

        private string _toolsName = string.Empty;
        public string ToolsName
        {
            get { return _toolsName; }
            set
            {
                _toolsName = value;
                OnPropertyChanged("ToolsName");
            }
        }

        private SolidColorBrush _background;
        public SolidColorBrush BackGround
        {
            get { return _background; }
            set
            {
                _background = value;
                OnPropertyChanged("BackGround");
            }
        }

        public ViewerToolBoxViewModel()
        {
            BackGround = Brushes.Green;
            UniformGridColumn = 2;
            UniformGridRow = 2;
            HeaderName = "Layout";
            ViewerToolBoxItemList = new ObservableCollection<IDataItem>();

            var itemA = new ButtonItem();
            itemA.Name = "Button";
            ViewerToolBoxItemList.Add(itemA);

            var itemB = new ButtonItem();
            itemB.Name = "Button";
            ViewerToolBoxItemList.Add(itemB);

            var itemC = new ButtonItem();
            itemC.Name = "Button";
            ViewerToolBoxItemList.Add(itemC);

            var itemD = new ButtonItem();
            itemD.Name = "Button";
            ViewerToolBoxItemList.Add(itemD);

            ToolsGridColumn = 2;
            ToolsGridRow = 7;
            ToolsName = "Tools";
            ViewerToolsItems = new ObservableCollection<IDataItem>();

            var toggleitemA = new ButtonItem();
            toggleitemA.Name = "Toggle";
            ViewerToolsItems.Add(toggleitemA);

            var toggleitemB = new ButtonItem();
            toggleitemB.Name = "Toggle";
            ViewerToolsItems.Add(toggleitemB);

            var toggleitemC = new ButtonItem();
            toggleitemC.Name = "Toggle";
            ViewerToolsItems.Add(toggleitemC);

            var toggleitemD = new ButtonItem();
            toggleitemD.Name = "Toggle";
            ViewerToolsItems.Add(toggleitemD);

            var toggleitemE = new ButtonItem();
            toggleitemE.Name = "Toggle";
            ViewerToolsItems.Add(toggleitemE);

            var toggleitemF = new ButtonItem();
            toggleitemF.Name = "Toggle";
            ViewerToolsItems.Add(toggleitemF);

            var toggleitemG = new ButtonItem();
            toggleitemG.Name = "Toggle";
            ViewerToolsItems.Add(toggleitemG);

            var toggleitemH = new ButtonItem();
            toggleitemH.Name = "Toggle";
            ViewerToolsItems.Add(toggleitemH);

            var comBoxItemA = new ComBoxItem();
            comBoxItemA.Name = "ComBox";
            ViewerToolsItems.Add(comBoxItemA);

            var comBoxItemB = new ComBoxItem();
            comBoxItemB.Name = "ComBox";
            ViewerToolsItems.Add(comBoxItemB);

            var comBoxItemC = new ComBoxItem();
            comBoxItemC.Name = "ComBox";
            ViewerToolsItems.Add(comBoxItemC);

            var comBoxItemD = new ComBoxItem();
            comBoxItemD.Name = "ComBox";
            ViewerToolsItems.Add(comBoxItemD);
        }

        public ObservableCollection<IDataItem> ViewerToolBoxItemList { get; set; }

        public ObservableCollection<IDataItem> ViewerToolsItems { get; set; }
    }
}

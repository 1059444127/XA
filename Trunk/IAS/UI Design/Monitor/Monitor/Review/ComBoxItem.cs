using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Monitor.Review
{
    public class ComBoxItem:BasicModel,IDataItem
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
            return "ComBox";
        }

        public ObservableCollection<Item> ComBoxItemList { get; set; }

        public ComBoxItem()
        {
            ComBoxItemList = new ObservableCollection<Item>();

            var itemA = new Item();
            itemA.Name = "Item";
            ComBoxItemList.Add(itemA);

            var itemB = new Item();
            itemB.Name = "Item";
            ComBoxItemList.Add(itemB);

            var itemC = new Item();
            itemC.Name = "Item";
            ComBoxItemList.Add(itemC);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Monitor.Review;

namespace Monitor.PA
{
    public class ListItemDataTemplateSelector : DataTemplateSelector
    {
        private DataTemplate _addItemDataTemplate = null;
        public DataTemplate AddItemDataTemplate
        {
            get { return _addItemDataTemplate; }
            set { _addItemDataTemplate = value; }
        }

        private DataTemplate _imgItemDataTemplate = null;
        public DataTemplate ImgItemDataTemplate
        {
            get { return _imgItemDataTemplate; }
            set { _imgItemDataTemplate = value; }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var dataItem = item as DataTemplateItem;
            if (dataItem != null && dataItem.GetType().Equals("Image"))
            {
                return ImgItemDataTemplate;
            }
            else if (dataItem != null && dataItem.GetType().Equals("Add"))
            {
                return AddItemDataTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}

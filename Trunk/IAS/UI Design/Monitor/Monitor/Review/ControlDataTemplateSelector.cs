using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Monitor.Review
{
    public class ControlDataTemplateSelector : DataTemplateSelector
    {
        private DataTemplate _buttonItemDataTemplate = null;
        public DataTemplate ButtonItemDataTemplate
        {
            get { return _buttonItemDataTemplate; }
            set { _buttonItemDataTemplate = value; }
        }

        private DataTemplate _toggleButtonDataTemplate = null;
        public DataTemplate ToggleButtonDataTemplate
        {
            get { return _toggleButtonDataTemplate; }
            set { _toggleButtonDataTemplate = value; }
        }

        private DataTemplate _comBoxDataTemplate = null;
        public DataTemplate ComBoxDataTemplate
        {
            get { return _comBoxDataTemplate; }
            set { _comBoxDataTemplate = value; }
        }

         public override DataTemplate SelectTemplate(object item, DependencyObject container)
         {
             var dataItem = item as IDataItem;
             if (dataItem != null && dataItem.GetItemType().Equals("Button"))
             {
                 return ButtonItemDataTemplate;
             }
             else if (dataItem != null && dataItem.GetItemType().Equals("ToggleButton"))
             {
                 return ToggleButtonDataTemplate;
             }
             else if (dataItem != null && dataItem.GetItemType().Equals("ComBox"))
             {
                 return ComBoxDataTemplate;
             }
             return base.SelectTemplate(item, container);
         }


    }
}

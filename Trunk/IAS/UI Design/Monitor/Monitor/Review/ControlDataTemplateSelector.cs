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
        private DataTemplate _controlDataTemplate = null;
        public DataTemplate ControlDataTemplate
        {
            get { return _controlDataTemplate; }
            set { _controlDataTemplate = value; }
        }

        private DataTemplate _contentDataTemplate = null;
        public DataTemplate ContentDataTemplate
        {
            get { return _contentDataTemplate; }
            set { _contentDataTemplate = value; }
        }

         public override DataTemplate SelectTemplate(object item, DependencyObject container)
         {
             if ((item as ItemViewModel) != null)
             {
                 return ControlDataTemplate;
             }
             else if ((item as ContentViewModel) != null)
             {
                 return ContentDataTemplate;
             }
             return base.SelectTemplate(item, container);
         }


    }
}

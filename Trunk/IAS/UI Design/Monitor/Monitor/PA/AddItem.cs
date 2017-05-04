using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Monitor.ViewModel;

namespace Monitor.PA
{
    public class AddItem : BasicModel, DataTemplateItem
    {

        public string GetType()
        {
            return "Add";
        }
    }
}

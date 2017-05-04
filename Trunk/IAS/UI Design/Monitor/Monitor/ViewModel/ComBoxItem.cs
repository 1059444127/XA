using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor.ViewModel
{
    public class ComBoxItem:BasicModel
    {
        public string Name { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}

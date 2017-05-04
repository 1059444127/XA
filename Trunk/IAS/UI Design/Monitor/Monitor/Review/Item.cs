using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor.Review
{
    public class Item :BasicModel
    {
        public override string ToString()
        {
            return Name;
        }

        public string Name { get; set; }
    }
}

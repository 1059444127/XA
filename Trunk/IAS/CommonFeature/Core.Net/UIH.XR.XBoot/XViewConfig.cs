using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UIH.XR.Core
{
    public class XViewConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string DataContext { get; set; }

        [XmlAttribute]
        public string Region { get; set; }
    }
}

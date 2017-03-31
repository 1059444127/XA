using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UIH.XR.Core
{
    public class XShellConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public bool ShowOnStartup { get; set; }

        [XmlAttribute]
        public string RootView { get; set; }

    }
}

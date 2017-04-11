using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UIH.XR.Core
{
    [XmlRoot]
    public class XAppConfig
    {
        [XmlArray, XmlArrayItem("Assembly")]
        public string[] Assemblies { get; set; }

        [XmlArray, XmlArrayItem("Shell")]
        public XShellConfig[] Shells { get; set; }

        [XmlArray, XmlArrayItem("Region")]
        public XRegionConfig[] Regions { get; set; }
    }
}

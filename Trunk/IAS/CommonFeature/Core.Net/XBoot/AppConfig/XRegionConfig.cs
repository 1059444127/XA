/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UIH.XA.Core
{
    public class XRegionConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string View { get; set; }

        [XmlAttribute]
        public string DataContext { get; set; }

        [XmlArray, XmlArrayItem("View")]
        public XRegionViewItemConfig[] Views { get; set; }

        public bool IsNavigable
        {
            get
            {
                bool isNavigable = false;
                if ((string.IsNullOrWhiteSpace(View) & string.IsNullOrWhiteSpace(DataContext)) && Views != null && Views.Length > 0)
                    isNavigable = true;

                return isNavigable;
            }
        }
    }


    public class XRegionViewItemConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string View { get; set; }

        [XmlAttribute]
        public string DataContext { get; set; }
    }
}

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

namespace UIH.XA.StateMachine.Default
{
    public class TransitionDefinition
    {
        [XmlAttribute]
        public string ID { get; set; }

        [XmlAttribute]
        public string From { get; set; }

        [XmlAttribute]
        public string To { get; set; }

        [XmlArray, XmlArrayItem("Action")]
        public ActionDefinition[] Actions { get; set; }
    }
}

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

namespace UIH.XR.StateMachine.Default
{
    public class StateDefinition
    {
        [XmlAttribute]
        public string ID { get; set; }

        [XmlAttribute]
        public StateCategory Category { get; set; }

        [XmlArray, XmlArrayItem("Action")]
        public ActionDefinition[] ActionsOnEntry { get; set; }

        [XmlArray, XmlArrayItem("Action")]
        public ActionDefinition[] ActionsOnExit { get; set; }

        public StateDefinition()
        {
            Category = StateCategory.NORMAL;
        }
    }
}

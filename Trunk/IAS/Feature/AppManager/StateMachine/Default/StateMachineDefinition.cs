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
    [XmlRoot("StateMachine")]
    public class StateMachineDefinition
    {
        [XmlArray, XmlArrayItem("State")]
        public StateDefinition[] States;

        [XmlArray, XmlArrayItem("Transition")]
        public TransitionDefinition[] Transitions;
        
        /// <summary>
        /// Assembly and namespace of actions, should be like "namesapce, assembly"
        /// </summary>
        [XmlAttribute]
        public string ActionPackage { get; set; }
    }
}

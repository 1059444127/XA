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
    /// <summary>
    /// Infomation to define an action 
    /// </summary>
    public class ActionDefinition
    {
        /// <summary>
        /// type of the action
        /// </summary>
        [XmlAttribute]
        public string Class { get; set; }
    }
}

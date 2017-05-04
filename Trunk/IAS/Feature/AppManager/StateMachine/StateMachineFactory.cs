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
using UIH.XA.Core;
using System.IO;

namespace UIH.XA.StateMachine
{
    /// <summary>
    /// Factory to create StateMachine instance.
    /// </summary>
    public abstract class StateMachineFactory
    {
        /// <summary>
        /// Construct an instance implemented IStateMachine interface.
        /// </summary>
        /// <param name="xmlFilePath">the file path of xml file which defines the StateMachine</param>
        /// <returns>instance implemented IStateMachine</returns>
        public abstract IStateMachine CreateStateMachineFromXml(string xmlFilePath);

        /// <summary>
        /// Construct an instance implemented IStateMachine interface.
        /// </summary>
        /// <param name="xmlStream">the xml stream which defines the StateMachine</param>
        /// <returns>instance implemented IStateMachine</returns>
        public abstract IStateMachine CreateStateMachineFromXml(Stream xmlStream);

        /// <summary>
        /// Override this method if you need to initialize your action.
        /// </summary>
        /// <param name="action">the action constructed from xml config</param>
        /// <returns>the action initialized</returns>
        protected virtual IAction InitializeAction(IAction action){
            return action;
        }
    }
}

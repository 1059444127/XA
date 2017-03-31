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

namespace UIH.XR.StateMachine
{
    /// <summary>
    /// Action mounted on state and transition
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// if the action is ready to execute
        /// </summary>
        /// <param name="args">the argument of transition event</param>
        /// <returns>true if ready</returns>
        bool CanExecute(Object args);

        /// <summary>
        /// execute action
        /// </summary>
        /// <param name="argv">the argument of transition event</param>
        void Execute(Object args);
    }
}

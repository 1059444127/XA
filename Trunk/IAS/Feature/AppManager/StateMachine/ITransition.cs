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

namespace UIH.XA.StateMachine
{
    /// <summary>
    /// Transition defines a path from a state to another state.
    /// </summary>
    public interface ITransition
    {
        /// <summary>
        /// Transition ID, any two transitions cann't have the same ID if they are
        /// in same state. 
        /// </summary>
        string ID { get; }

        /// <summary>
        /// The state this transition path from.
        /// </summary>
        IState From { get; }

        /// <summary>
        /// The state this transition path to.
        /// </summary>
        IState To { get; }

        /// <summary>
        /// Actions mounted on this transition.
        /// </summary>
        List<IAction> Actions { get; }
    }
}

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
    /// State machine can handle message and do transition.Any time, the state machine 
    /// has an deterministic state.
    /// </summary>
    public interface IStateMachine
    {
        /// <summary>
        /// Do transition
        /// </summary>
        /// <param name="transitionID">the ID of transition</param>
        /// <param name="args">transition argument</param>
        void Transit(string transitionID, object args);

        /// <summary>
        /// Check if the state machine is busy. A state machine is busy only if the Transit
        /// method is invoked and does not return.
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// The current state.
        /// </summary>
        IState CurrentState { get; }
    }
}

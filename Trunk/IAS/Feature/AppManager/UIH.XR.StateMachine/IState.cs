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
using UIH.XR.Core;

namespace UIH.XR.StateMachine
{
    /// <summary>
    /// State in state machine.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// ID of state, should be semantics. 
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Handle event and do transition.
        /// </summary>
        /// <param name="transitionID">transition ID</param>
        /// <param name="args">transition event argument</param>
        /// <returns>the next state</returns>
        IState Transit(string transitionID, object args);

        /// <summary>
        /// Check if all the actions mounted on this state are ready to transit.
        /// </summary>
        /// <param name="transitionID">transition ID</param>
        /// <param name="args">transition event argument</param>
        /// <returns>true if ready</returns>
        bool IsReadyToTransit(string transitionID, object args);

        /// <summary>
        /// Register transition to this state. Any two transitions.
        /// on same state cannot have the same ID.
        /// </summary>
        /// <param name="transition">transition</param>
        void RegisterTransition(ITransition transition);
        
        /// <summary>
        /// If the state is ready to enter, which means the actions on entry.
        /// all return true.
        /// </summary>
        /// <param name="args">transition argument</param>
        /// <returns>true if ready</returns>
        bool IsReadyToEnter(object args);

        /// <summary>
        /// Invoke actions on entry.
        /// </summary>
        /// <param name="args">transition argument</param>
        void Enter(object args);

        /// <summary>
        /// Invoke actions on exit.
        /// </summary>
        /// <param name="args">transition argument</param>
        void Exit(object args);

        /// <summary>
        /// If the transition with specified ID is registered to this state.
        /// </summary>
        /// <param name="transitionID">ID</param>
        /// <returns>true if registered</returns>
        bool IsTransitionRegistered(string transitionID);
    }
}

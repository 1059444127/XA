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
using System.Reflection;
using UIH.XR.Core;
using UIH.Mcsf.Log;

namespace UIH.XR.StateMachine.Default
{
    public class State : IState
    {
        private Dictionary<string, ITransition> _transitions = new Dictionary<string, ITransition>();

        private List<IAction> _actionsOnEntry = new List<IAction>();

        private List<IAction> _actionsOnExit = new List<IAction>();

        private ActionBuilder _actionBuilder;

        public StateCategory Category { get; set; }

        public string ID { get; private set; }

        public State(ActionBuilder actionBuilder)
        {
            Category = StateCategory.NORMAL;
            _actionBuilder = actionBuilder;
        }

        public void Initialize(StateDefinition definition)
        {
            ID = definition.ID;
            Category = definition.Category;
            if (definition.ActionsOnEntry == null) return;
            foreach (ActionDefinition actionDef in definition.ActionsOnEntry)
            {
                CLRLogger.GetInstance().LogDevInfo(string.Format("Load {0}", actionDef.Class));
                try
                {
                    IAction action = _actionBuilder.Build(actionDef);
                    _actionsOnEntry.Add(action);
                }
                catch (Exception ex)
                {
                    CLRLogger.GetInstance().LogDevInfo(string.Format("Failed to load {0}", actionDef.Class));
                    CLRLogger.GetInstance().LogDevError(ex.Message);
                }
            }
            if (definition.ActionsOnExit == null) return;
            foreach (ActionDefinition actionDef in definition.ActionsOnExit)
            {
                CLRLogger.GetInstance().LogDevInfo(string.Format("Load {0}", actionDef.Class));
                try
                {
                    IAction action = _actionBuilder.Build(actionDef);
                    _actionsOnExit.Add(action);
                }
                catch (Exception ex)
                {
                    CLRLogger.GetInstance().LogDevInfo(string.Format("Failed to load {0}", actionDef.Class));
                    CLRLogger.GetInstance().LogDevError(ex.Message);
                }
            }
        }

        public IState Transit(string transitionID, object eventArgs)
        {
            ITransition transition = FindTransition(transitionID);
            if (transition == null) return null;
            if (!transition.To.Equals(this))
            {
                Exit(eventArgs);
                transition.Actions.ForEach(ax => ax.Execute(eventArgs));
            }
            return transition.To;
        }

        public bool IsReadyToTransit(string transitionID, object eventArgs)
        {
            ITransition transaction = FindTransition(transitionID);
            if (transaction == null)
            {
                CLRLogger.GetInstance().LogDevWarning(string.Format("Unknown event {0}", transitionID));
                return false;
            }
            List<IAction> actions = new List<IAction>();
            actions.AddRange(_actionsOnExit);
            actions.AddRange(transaction.Actions);
            foreach (IAction action in actions)
            {
                if (!action.CanExecute(eventArgs))
                {
                    CLRLogger.GetInstance().LogSvcError(string.Format("Action {0} report itself cannot be executed", action.GetType().Name));
                    return false;
                }
            }
            return true;
        }

        public bool IsReadyToEnter(object eventArgs)
        {
            foreach (IAction action in _actionsOnExit)
            {
                if (!action.CanExecute(eventArgs))
                {
                    CLRLogger.GetInstance().LogSvcError(string.Format("Action {0} report itself cannot be executed", action.GetType().Name));
                    return false;
                }
            }
            return true;
        }

        public void RegisterTransition(ITransition transition)
        {
            if (_transitions.ContainsKey(transition.ID))
            {
                throw new StateMachineException("try to add repeat transition");
            }
            _transitions.Add(transition.ID, transition);
        }

        public bool IsTransitionRegistered(string transitionID)
        {
            return FindTransition(transitionID) != null;
        }

        private ITransition FindTransition(string transitionID)
        {
            if (transitionID == null) return null;
            if (!_transitions.ContainsKey(transitionID)) return null;
            return _transitions[transitionID];
        }

        public override bool Equals(object obj)
        {
            if (!(obj is State)) return false;
            State that = obj as State;
            return that.ID == this.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public void Enter(object eventArgs)
        {
            _actionsOnEntry.ForEach(ax => ax.Execute(eventArgs));
        }

        public void Exit(object eventArgs)
        {
            _actionsOnExit.ForEach(ax => ax.Execute(eventArgs));
        }
    }
}

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
using System.Threading;
using UIH.Mcsf.Log;

namespace UIH.XR.StateMachine.Default
{
    public class DefaultStateMachine : IStateMachine
    {
        const int ACTION_HEARBEAT_COUNT = 10;
        const int ACTION_HEATBEAT_INTERVAL = 1000;//ms

        public DefaultStateMachine()
        {
            States = new HashSet<State>();
        }

        /// <summary>
        /// Initialize by StateMachineDefinition instance.
        /// </summary>
        /// <param name="def">StateMachineDefinition instance</param>
        /// <param name="logger">the logger used to</param>
        /// <param name="actionInitializer"></param>
        public void Initialize(StateMachineDefinition def, Func<IAction, IAction> actionInitializer)
        {
            _actionBuilder = new ActionBuilder(def.ActionPackage, actionInitializer);
            foreach (StateDefinition stateDef in def.States)
            {
                CLRLogger.GetInstance().LogDevInfo(string.Format("Load State {0}", stateDef.ID));
                State state = new State(_actionBuilder);
                state.Initialize(stateDef);
                if (States.Contains(state))
                {
                    throw new StateMachineException(string.Format("try to add an existed state {0}", state.ID));
                }
                States.Add(state);
                if (state.Category == StateCategory.INITIAL)
                {
                    CurrentState = state;
                }
            }
            foreach (TransitionDefinition tranDef in def.Transitions)
            {
                LoadTransaction(tranDef);
            }
        }

        public void Transit(string transitionID, object args)
        {
            CLRLogger.GetInstance().LogDevInfo(string.Format("Begin transition {0}, state = {1}", transitionID, CurrentState.ID));
            if (transitionID == null)
            {
                CLRLogger.GetInstance().LogDevError("null event");
            }
            if (!CurrentState.IsTransitionRegistered(transitionID))
            {
                CLRLogger.GetInstance().LogDevWarning(string.Format("invalid transaction {0} for state {1}", transitionID, CurrentState.ID));
                return;
            }
            int i = ACTION_HEARBEAT_COUNT;
            while(!CurrentState.IsReadyToTransit(transitionID, args) && i>0)
            {
                if (i == 0) return;
                CLRLogger.GetInstance().LogSvcError("pause to transact");
                Thread.Sleep(ACTION_HEATBEAT_INTERVAL);
                i--;
            }
            IState nextState = CurrentState.Transit(transitionID, args);
            i = ACTION_HEARBEAT_COUNT;
            while (!nextState.IsReadyToEnter(args) && i > 0)
            {
                if (i == 0) return;
                CLRLogger.GetInstance().LogSvcError("pause to enter " + nextState.ID);
                Thread.Sleep(ACTION_HEATBEAT_INTERVAL);
                i--;
            }
            CurrentState = nextState;
            CurrentState.Enter(args);
            CLRLogger.GetInstance().LogDevInfo(string.Format("End transition {0}, state = {1}", transitionID, CurrentState.ID));
        }

        private bool _isBusy = false;

        public bool IsBusy
        {
            get 
            {
                lock (this)
                {
                    return _isBusy;
                }
            }
            set
            {
                lock (this)
                {
                    _isBusy = value;
                }
            }
        }

        public IState CurrentState { get; private set; }

        public HashSet<State> States { get; private set; }

        private ActionBuilder _actionBuilder;

        private State GetStateById(string stateId)
        {
            foreach (State state in States)
            {
                if (state.ID == stateId) return state;
            }
            return null;
        }

        private void LoadTransaction(TransitionDefinition def)
        {
            CLRLogger.GetInstance().LogDevInfo(string.Format("Load Transaction {0}:{1}->{2}", def.ID, def.From, def.To));
            Transition tran = new Transition();
            tran.ID = def.ID;
            tran.From = GetStateById(def.From);
            if (tran.From == null)
            {
                if (def.From == "*")
                {
                    foreach (State state in States)
                    {
                        TransitionDefinition tranDef = new TransitionDefinition();
                        tranDef.ID = def.ID;
                        tranDef.From = state.ID;
                        tranDef.Actions = def.Actions;
                        tranDef.To = def.To;
                        if (tranDef.To == "*") tranDef.To = state.ID;
                        LoadTransaction(tranDef);
                    }
                }
                else
                {
                    throw new StateMachineException(string.Format("Invalid TransactionDefinition {0}:{1}->{2}, no such state from", def.ID, def.From, def.To));
                }
                return;
            }
            tran.From.RegisterTransition(tran);
            if (tran.To == null)
            {
                tran.To = GetStateById(def.To);
            }
            if (tran.To == null)
            {
                throw new StateMachineException(string.Format("Invalid TransactionDefinition {0}:{1}->{2}, no such state to", def.ID, def.From, def.To));
            }
            if (def.Actions == null) return;
            foreach (ActionDefinition actionDef in def.Actions)
            {
                CLRLogger.GetInstance().LogDevInfo(string.Format("Load action {0}", actionDef.Class));
                tran.Actions.Add(_actionBuilder.Build(actionDef));
            }
        }
    }
}

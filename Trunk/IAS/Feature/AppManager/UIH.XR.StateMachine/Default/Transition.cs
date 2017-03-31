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

namespace UIH.XR.StateMachine.Default
{
    class Transition : ITransition
    {
        public string ID { get; set; }

        public IState From { get; set; }

        public IState To { get; set; }

        public List<IAction> Actions { get; protected set; }

        public Transition()
        {
            Actions = new List<IAction>();
        }

    }
}

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
    public class StateMachineException : Exception
    {
        public StateMachineException(string msg, Exception innerException) 
            : base(msg, innerException) { }

        public StateMachineException(string msg) : base(msg) { }
    }
}

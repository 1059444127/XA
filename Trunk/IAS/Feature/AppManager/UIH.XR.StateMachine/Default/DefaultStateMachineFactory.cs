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
using System.IO;
using UIH.XR.Core;

namespace UIH.XR.StateMachine.Default
{
    public class DefaultStateMachineFactory : StateMachineFactory
    {
        public override IStateMachine CreateStateMachineFromXml(string xmlFilePath)
        {
            using (Stream xmlFileStream = File.OpenRead(xmlFilePath))
            {
                return CreateStateMachineFromXml(xmlFileStream);
            }
        }

        public override IStateMachine CreateStateMachineFromXml(Stream xmlStream)
        {
            if (xmlStream == null) throw new StateMachineException("xmlStream");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(StateMachineDefinition));
            StateMachineDefinition def = xmlSerializer.Deserialize(xmlStream) as StateMachineDefinition;
            string error;
            if (!IsValidDefinition(def, out error))
            {
                throw new StateMachineException("Invalid StateMachineDefinition for " + error);
            }
            DefaultStateMachine sm = new DefaultStateMachine();
            sm.Initialize(def, InitializeAction);
            return sm;
        }

        private bool IsValidDefinition(StateMachineDefinition def, out string error)
        {
            error = "";
            if (def.ActionPackage == null || !def.ActionPackage.Contains(","))
            {
                error = "ActionPacakge not set or format invalid";
                return false;
            }
            if (def.States == null || def.Transitions == null)
            {
                error = "States or Transactions not set";
                return false;
            }
            foreach (StateDefinition stateDef in def.States)
            {
                if (string.IsNullOrWhiteSpace(stateDef.ID))
                {
                    error = "State Id not set or format invalid";
                    return false;
                }
            }
            foreach (TransitionDefinition tranDef in def.Transitions)
            {
                if (string.IsNullOrWhiteSpace(tranDef.ID) 
                    || string.IsNullOrWhiteSpace(tranDef.From) 
                    || string.IsNullOrWhiteSpace(tranDef.To))
                {
                    error = "invalid TransactionDefinition";
                    return false;
                }
            }
            return true;
        }
    }
}

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

namespace UIH.XR.Core
{
    public class XWorkflow : IWorkflow
    {

        #region Properties

        private string _currentProcedure;
        public string CurrentProcedure
        {
            get { return _currentProcedure; }
        }

        #endregion


        #region Constructor



        #endregion


        #region Methods


        public bool Invoke(string procedure, object context)
        {
            return true;
        }

        private void RaiseProcedureChanged(string oldProcedure, string newProcedure)
        {
            if (ProcedureChanged != null)
                ProcedureChanged(oldProcedure, newProcedure);
        }

        #endregion


        #region Events

        public event ProcedureChangedHandle ProcedureChanged;

        #endregion

    }
}

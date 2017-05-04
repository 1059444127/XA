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
using System.Windows.Input;

namespace UIH.XA.XSample.ViewModels
{
    public class SampleCommand : ICommand
    {
        Action<object> _method;

        public SampleCommand(Action<object> method)
        {
            _method = method;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_method != null)
                _method(parameter);
        }
    }
}

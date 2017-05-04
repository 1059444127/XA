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
using System.ComponentModel.Composition;
using System.ComponentModel;
using UIH.XA.Core;

namespace UIH.XA.XSample.ViewModels
{
    [Export("SmartViewModel")]
    public class SmartViewModel : NotificationObject
    {
        public SmartViewModel()
        {
            _description = "SMART";
            OneCommand = new SampleCommand((o) => { ShowHide("WindowOne", ref IsOne); });
            TwoCommand = new SampleCommand((o) => { ShowHide("WindowTwo", ref IsTwo); });
        
        }


        [Import(typeof(IShellManager))]
        public IShellManager ShellManager { get; set; }


        private bool IsOne = false;
        private bool IsTwo = false;

        public SampleCommand OneCommand { get; set; }

        public SampleCommand TwoCommand { get; set; }

        private void ShowHide(string shellname,ref bool isShow)
        {
           IShell shell= ShellManager.GetShell(shellname);
           if (shell != null)
           {
               if (!isShow)
                   shell.ShowShell();
               else
                   shell.HideShell();

               isShow = !isShow;
           }
        }

    }
}

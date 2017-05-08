using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using UIH.XA.Core;

namespace UIH.XA.PatientAdmin.Shells
{
   public class PaShell:Window,IShell
    {
       public bool ShowShell()
       {
           this.Show();
           this.Activate();
           return true;
       }

       public bool HideShell()
       {
           this.Hide();
           return true;
       }

       public bool BlockShell()
       {
           this.IsEnabled = false;
           return true;
       }

       public bool UnblockShell()
       {
           this.IsEnabled = true;
           return true;
       }

       public string ShellName
       {
           get;
           set;
       }
    }
}

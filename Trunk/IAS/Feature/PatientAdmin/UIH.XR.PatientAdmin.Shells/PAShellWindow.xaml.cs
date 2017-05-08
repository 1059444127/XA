using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIH.XA.Core;

namespace UIH.XA.PatientAdmin.Shells
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
     [Export("PAShellWindow", typeof(IShell))]
    public partial class PAShellWindow : PaShell
    {
         public PAShellWindow()
        {
            InitializeComponent();
        }
    }
}

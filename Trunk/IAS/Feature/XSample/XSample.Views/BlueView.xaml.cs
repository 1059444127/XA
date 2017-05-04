using System;
using System.Collections.Generic;
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
using System.ComponentModel.Composition;

namespace UIH.XA.XSample.Views
{
    /// <summary>
    /// Interaction logic for BlueView.xaml
    /// </summary>
    [Export("BlueView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class BlueView : UserControl
    {
        public BlueView()
        {
            InitializeComponent();
        }
    }
}

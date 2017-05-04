using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using UIH.XA.Core;
using Microsoft.Practices.Prism.Regions;

namespace UIH.XA.XSample.Shells
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export("MainShell", typeof(IShell))]
    public partial class MainShell : XShell
    {
        public MainShell()
        {
            Debugger.Launch();
            InitializeComponent();
            //RegionManager.SetRegionName(contentLeft, "MainLeftRegion");
            //RegionManager.SetRegionName(contentRightUp, "MainRightUpRegion");
            //RegionManager.SetRegionName(contentRightDown, "MainRightDownRegion");
        }
    }
}

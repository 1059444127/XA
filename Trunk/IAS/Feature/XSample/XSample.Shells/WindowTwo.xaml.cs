﻿using System;
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
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using UIH.XA.Core;

namespace UIH.XA.XSample.Shells
{
    /// <summary>
    /// Interaction logic for WindowTwo.xaml
    /// </summary>
    [Export("WindowTwo", typeof(IShell))]
    public partial class WindowTwo : XShell
    {
        public WindowTwo()
        {
            InitializeComponent();
        }
    }
}

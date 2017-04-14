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
using UIH.XR.XSample.Utility;

namespace UIH.XR.XSample.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [Export("YellowView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class YellowView : UserControl
    {
        public YellowView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(YellowView_Loaded);
        }

        void YellowView_Loaded(object sender, RoutedEventArgs e)
        {
           
            
            IList<Button> btnList = VisualElementHepler.FindVisualChild<Button>(this);
            foreach (var btn in btnList)
            {
                ButtonBehavior bb1 = new ButtonBehavior();
                ButtonBehavior bb2 = new ButtonBehavior();
                bb2.OutputFormat = "{0}.{1} click record.";
                bb1.Attach(btn);
                bb2.Attach(btn);
            }
        }


    }
}

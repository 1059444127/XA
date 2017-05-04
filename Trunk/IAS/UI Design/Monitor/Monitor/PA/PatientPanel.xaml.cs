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

namespace Monitor.PA
{
    /// <summary>
    /// Interaction logic for PatientPanel.xaml
    /// </summary>
    public partial class PatientPanel : UserControl
    {
        public PatientPanel()
        {
            InitializeComponent();
        }

        private void Emergency_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in Application.Current.Windows)
            {
                var itemWindow = item as Window;
                if (itemWindow.Title.Equals("exam_PA"))
                {
                    itemWindow.Close();
                }
            }
        }
    }
}

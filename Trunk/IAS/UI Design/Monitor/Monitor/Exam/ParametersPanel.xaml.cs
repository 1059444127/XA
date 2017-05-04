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
using Monitor.windows;

namespace Monitor.Exam
{
    /// <summary>
    /// Interaction logic for ParametersPanel.xaml
    /// </summary>
    public partial class ParametersPanel : UserControl
    {
   
        public ParametersPanel()
        {
            InitializeComponent();
        }

        private void ShowPaWindowBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var window = Utilities.FindAncestor<Window>(e.OriginalSource as DependencyObject);

            foreach (var item in Application.Current.Windows)
            {
                var itemWindow = item as Window;
                if (itemWindow.Title.Equals("exam_PA"))
                {
                    return;
                }
            }
            if (window.Title.Equals("exam"))
            {
                var paExam = new exam_PA();
                paExam.Topmost = true;
                paExam.Show();
            }
        }

        private void SystemSettingBtn_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in Application.Current.Windows)
            {
                var itemWindow = item as Window;
                if (itemWindow.Title.Equals("exam_Configuration"))
                {
                    return;
                }
            }

            var configuration = new exam_Configuration();
            configuration.Topmost = true;
            configuration.Show();
        }

        private void InformationBtn_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in Application.Current.Windows)
            {
                var itemWindow = item as Window;
                if (itemWindow.Title.Equals("exam_Infomation"))
                {
                    return;
                }
            }

            var Infomation = new exam_Infomation();
            Infomation.Topmost = true;
            Infomation.Show();
        }
    }
}

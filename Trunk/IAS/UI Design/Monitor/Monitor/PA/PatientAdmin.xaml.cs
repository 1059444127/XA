using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Monitor.windows;
using Application = System.Windows.Application;
using UserControl = System.Windows.Controls.UserControl;

namespace Monitor.PA
{
    /// <summary>
    /// Interaction logic for PatientAdmin.xaml
    /// </summary>
    public partial class PatientAdmin : UserControl
    {
        public PatientAdmin()
        {
            InitializeComponent();
        }

        private void Exam_OnClick(object sender, RoutedEventArgs e)
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

        private void Icon_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var windows = Application.Current.Windows;
                foreach (var item in windows)
                {
                    var itemWindow = item as Window;
                    if (itemWindow.Title.Equals("exam_Reference")
                        || itemWindow.Title.Equals("exam_Review"))
                    {
                        itemWindow.Close();
                    }
                }

                var screens = Screen.AllScreens;
                var review = new exam_Review();
                review.WindowStartupLocation = WindowStartupLocation.Manual;

                if (screens.Count() == 1)
                {
                    var bound = screens.FirstOrDefault().Bounds;
                    review.Top = bound.Top;
                    review.Left = bound.Width / 2.0;
                    review.Height = bound.Height;
                    review.Width = bound.Width / 2.0;
                    review.Show();
                }
                else
                {
                    var primaryScreen = screens[1];
                    var primaryWorkingArea = primaryScreen.Bounds;

                    review.Top = primaryWorkingArea.Top;
                    review.Left = primaryWorkingArea.Left;
                    review.Height = primaryWorkingArea.Height;
                    review.Width = primaryWorkingArea.Width;
                    review.Show();
                }
            }
        }
    }
}

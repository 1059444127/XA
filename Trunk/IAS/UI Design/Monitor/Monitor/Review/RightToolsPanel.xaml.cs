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

namespace Monitor.Review
{
    /// <summary>
    /// Interaction logic for RightToolsPanel.xaml
    /// </summary>
    public partial class RightToolsPanel : UserControl
    {
        public RightToolsPanel()
        {
            InitializeComponent();
        }
		private void MainWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();// 在此处添加事件处理程序实现。
            wnd.Show();
        }

        private void ShowImageBroswerBtn_OnClick(object sender, RoutedEventArgs e)
        {

            var windows = Application.Current.Windows;
            foreach (var item in windows)
            {
                var itemWindow = item as Window;
                if (itemWindow.Title.Equals("exam_ImageBroswer"))
                {
                    return;
                }
            }

            var window = Utilities.FindAncestor<Window>(e.OriginalSource as DependencyObject);

            if (window.Title.Equals("exam_Reference") ||
                window.Title.Equals("exam_Compared") || window.Title.Equals("exam_Review"))
            {
                var examImageBroswer = new exam_ImageBroswer();
                examImageBroswer.Topmost = true;
                examImageBroswer.Show();
            }
        }

        private void CompleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var windows = Application.Current.Windows;
            foreach (var item in windows)
            {
                var itemWindow = item as Window;
                if (itemWindow == null)
                {
                    continue;
                }
                if (itemWindow.Title.Equals("exam_Compared"))
                {
                    itemWindow.Close();
                }
            }

            bool containExam = false;
            bool containexamReference = false;
            foreach (var item in windows)
            {
                var itemWindow = item as Window;
                if (itemWindow == null)
                {
                    continue;
                }
                if (itemWindow.Title.Equals("exam"))
                {
                    containExam = true;
                }
                if (itemWindow.Title.Equals("exam_Reference"))
                {
                    containexamReference = true;
                }
            }
            if (containExam && containexamReference)
            {
                return;
            }

            var screens = Screen.AllScreens;

            var exam = new exam();
            exam.WindowStartupLocation = WindowStartupLocation.Manual;

            var examReference = new exam_Reference();
            examReference.WindowStartupLocation = WindowStartupLocation.Manual;

            if (screens.Count() == 1)
            {
                var bound = screens.FirstOrDefault().Bounds;
                exam.Top = bound.Top;
                exam.Left = bound.Left;
                exam.Height = bound.Height;
                exam.Width = bound.Width / 2.0;
                exam.Show();

                examReference.Top = bound.Top;
                examReference.Left = bound.Width / 2.0;

                examReference.Height = bound.Height;
                examReference.Width = bound.Width / 2.0;

                examReference.Show();
            }
            else
            {
                var secondaryScreen = screens[0];
                var secondaryWorkingArea = secondaryScreen.Bounds;

                exam.Top = secondaryWorkingArea.Top;
                exam.Left = secondaryWorkingArea.Left;
                exam.Height = secondaryWorkingArea.Height;
                exam.Width = secondaryWorkingArea.Width;
                //exam.WindowState = System.Windows.WindowState.Maximized;
                exam.Show();

                var primaryScreen = screens[1];
                var primaryWorkingArea = primaryScreen.Bounds;

                examReference.Top = primaryWorkingArea.Top;
                examReference.Left = primaryWorkingArea.Left;
                examReference.Height = primaryWorkingArea.Height;
                examReference.Width = primaryWorkingArea.Width;
                //examReference.WindowState = primaryScreen.
                examReference.Show();
            }
        }
    }
}

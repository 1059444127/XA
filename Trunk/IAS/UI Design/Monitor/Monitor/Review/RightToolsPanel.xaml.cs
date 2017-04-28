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

        private void ShowImageBroswerBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //var window = Utilities.FindAncestor<Window>(e.OriginalSource as DependencyObject);

            //if (window.Title.Equals("exam_Reference") ||
            //    window.Title.Equals("exam_Compared"))
            //{
            //    var examImageBroswer = new exam_ImageBroswer();
            //    examImageBroswer.Topmost = true;
            //    examImageBroswer.Show();
            //}
        }

        private void CompleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //var windows = Application.Current.Windows;
            //foreach (var item in windows)
            //{
            //    var itemWindow = item as Window;
            //    if (itemWindow == null)
            //    {
            //        continue;
            //    }
            //    if (itemWindow.Title.Equals("exam_Compared"))
            //    {
            //        itemWindow.Close();
            //    }
            //}

            //bool containExam = false;
            //bool containexamReference = false;
            //foreach (var item in windows)
            //{
            //    var itemWindow = item as Window;
            //    if (itemWindow == null)
            //    {
            //        continue;
            //    }
            //    if (itemWindow.Title.Equals("exam"))
            //    {
            //        containExam = true;
            //    }
            //    if (itemWindow.Title.Equals("exam_Reference"))
            //    {
            //        containexamReference = true;
            //    }
            //}
            //if (containExam && containexamReference)
            //{
            //    return;
            //}

            //var screens = Screen.AllScreens;
            //var secondaryScreen = screens[1];
            //var secondaryWorkingArea = secondaryScreen.WorkingArea;
            //var exam = new exam();
            //exam.WindowStartupLocation = WindowStartupLocation.Manual;
            //exam.Top = secondaryWorkingArea.Top;
            //exam.Left = secondaryWorkingArea.Left;
            //exam.Height = secondaryWorkingArea.Height;
            //exam.Width = secondaryWorkingArea.Width;
            //exam.Show();

            //var primaryScreen = screens[0];
            //var primaryWorkingArea = primaryScreen.WorkingArea;
            //var examReference = new exam_Reference();
            //examReference.WindowStartupLocation = WindowStartupLocation.Manual;
            //examReference.Top = primaryWorkingArea.Top;
            //examReference.Left = primaryWorkingArea.Left;
            //examReference.Height = primaryWorkingArea.Height;
            //examReference.Width = primaryWorkingArea.Width;
            //examReference.Show();
        }
    }
}

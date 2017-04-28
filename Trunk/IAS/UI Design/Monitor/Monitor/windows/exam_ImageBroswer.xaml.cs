using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application = System.Windows.Application;

namespace Monitor.windows
{
    /// <summary>
    /// Interaction logic for exam_ImageBroswer.xaml
    /// </summary>
    public partial class exam_ImageBroswer : Window
    {
        public exam_ImageBroswer()
        {
            InitializeComponent();
        }
        private void drag(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();// 在此处添加事件处理程序实现。
        }

        private void ComparedBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //var windows = Application.Current.Windows;
            //foreach (var item in windows)
            //{
            //    var itemWindow = item as Window;
            //    if (itemWindow == null)
            //    {
            //        continue;
            //    }
            //    if (itemWindow.Title.Equals("exam")
            //        || itemWindow.Title.Equals("exam_Reference"))
            //    {
            //        itemWindow.Close();
            //    }
            //}
            //foreach (var item in windows)
            //{
            //    var itemWindow = item as Window;
            //    if (itemWindow == null)
            //    {
            //        continue;
            //    }
            //    if (itemWindow.Title.Equals("exam_Compared"))
            //    {
            //        return;
            //    }
            //}

            //var examCompared = new exam_Compared();
            //examCompared.WindowStartupLocation = WindowStartupLocation.Manual;
            //Rect workArea = SystemParameters.WorkArea;
            //examCompared.Width = SystemParameters.PrimaryScreenWidth * 2;
            //examCompared.Height = workArea.Bottom;
            //examCompared.Left = Screen.AllScreens[1].WorkingArea.Left;
            //examCompared.Top = Screen.AllScreens[1].WorkingArea.Top;
            //examCompared.Show();
        }

        private void SelectBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

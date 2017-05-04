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

        private void SelectBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            this.Window.Close();
        }

        private void ComparedBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var windows = Application.Current.Windows;
            foreach (var item in windows)
            {
                var itemWindow = item as Window;
                if (itemWindow == null)
                {
                    continue;
                }
                if (itemWindow.Title.Equals("exam")
                    || itemWindow.Title.Equals("exam_Reference") || itemWindow.Title.Equals("exam_ImageBroswer"))
                {
                    itemWindow.Close();
                }
            }
            foreach (var item in windows)
            {
                var itemWindow = item as Window;
                if (itemWindow == null)
                {
                    continue;
                }
                if (itemWindow.Title.Equals("exam_Compared"))
                {
                    return;
                }
            }

            var screen = Screen.AllScreens;
            var examCompared = new exam_Compared();
            examCompared.WindowStartupLocation = WindowStartupLocation.Manual;
            if (screen.Count() == 1)
            {
                var bound = screen.FirstOrDefault().Bounds;
                examCompared.Width = bound.Width;
                examCompared.Height = bound.Height;
                examCompared.Left = bound.Left;
                examCompared.Top = bound.Top;
                examCompared.Show();
            }
            else
            {
                var bound = screen.Where(x => x.Primary.Equals(true)).FirstOrDefault().Bounds;
                examCompared.Width = bound.Width * 2;
                examCompared.Height = bound.Height;
                examCompared.Left = bound.Left;
                examCompared.Top = bound.Top;
                examCompared.Show();
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

        private void Referenceicon_OnMouseDown(object sender, MouseButtonEventArgs e)
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

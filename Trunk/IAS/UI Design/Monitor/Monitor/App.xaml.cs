using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Monitor.windows;
using Application = System.Windows.Application;

namespace Monitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
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
                exam.Width = bound.Width/2.0;
                exam.Show();

                examReference.Top = bound.Top;
                examReference.Left = bound.Width/2.0;

                examReference.Height = bound.Height;
                examReference.Width = bound.Width/2.0;

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

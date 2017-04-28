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

            //var screens = Screen.AllScreens;

            //var exam = new exam();
            //exam.WindowStartupLocation = WindowStartupLocation.Manual;

            //var examReference = new exam_Reference();
            //examReference.WindowStartupLocation = WindowStartupLocation.Manual;

            //if (screens.Count() == 1)
            //{
                
            //}
            //else
            //{
            //    var secondaryScreen = screens[1];
            //    var secondaryWorkingArea = secondaryScreen.Bounds;

            //    exam.Top = secondaryWorkingArea.Top;
            //    exam.Left = secondaryWorkingArea.Left;
            //    exam.Height = secondaryWorkingArea.Height;
            //    exam.Width = secondaryWorkingArea.Width;
            //    exam.Show();

            //    var primaryScreen = screens[0];
            //    var primaryWorkingArea = primaryScreen.Bounds;

            //    examReference.Top = primaryWorkingArea.Top;
            //    examReference.Left = primaryWorkingArea.Left;
            //    examReference.Height = primaryWorkingArea.Height;
            //    examReference.Width = primaryWorkingArea.Width;
            //    examReference.Show();
            //}
           
        }
    }
}

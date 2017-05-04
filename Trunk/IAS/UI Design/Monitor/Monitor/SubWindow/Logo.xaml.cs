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

namespace Monitor.SubWindow
{
    /// <summary>
    /// Interaction logic for Logo.xaml
    /// </summary>
    public partial class Logo : UserControl
    {
        public Logo()
        {
            InitializeComponent();
        }
        private void MainWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();// 在此处添加事件处理程序实现。
            wnd.Show();
        }
    }
}

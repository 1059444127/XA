using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void close(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();// 在此处添加事件处理程序实现。
        }

        private void drag(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();// 在此处添加事件处理程序实现。
        }
        private void maxWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            this.WindowState = System.Windows.WindowState.Maximized;// 在此处添加事件处理程序实现。
        }

        private void minWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Normal;
            //this.ResizeMode=System.Windows.ResizeMode.CanResizeWithGrip;// 开启窗体大小拖动。
        }
    }
}

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
using System.Windows.Shapes;

namespace Monitor.windows
{
    /// <summary>
    /// Interaction logic for exam_Infomation.xaml
    /// </summary>
    public partial class exam_Infomation : Window
    {
        public exam_Infomation()
        {
            InitializeComponent();
        }

        private void drag(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();// 在此处添加事件处理程序实现。
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            this.Window.Close();
        }
    }
}

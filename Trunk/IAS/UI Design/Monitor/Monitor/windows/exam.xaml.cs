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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Monitor.windows
{
    /// <summary>
    /// Interaction logic for exam.xaml
    /// </summary>
    public partial class exam : Window
    {
        public exam()
        {
            InitializeComponent();
            Utilities.ResourceKey = "ButtonEffect";
        }

        //private void drag(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    this.DragMove();// 在此处添加事件处理程序实现。
        //}

        private void Exam_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var elements = Utilities.GetAllChildsElement<Button>(this.Window);
            Utilities.CancelVisualEffects(elements);
        }

        private void Exam_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var elements = Utilities.GetAllChildsElement<Button>(this.Window);
            Utilities.AddVisualEffects(elements);
        }
    }
}

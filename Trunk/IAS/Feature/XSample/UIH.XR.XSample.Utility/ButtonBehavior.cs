/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;

namespace UIH.XR.XSample.Utility
{
    public class ButtonBehavior : Behavior<Button>
    {

        public ButtonBehavior()
        {
            OutputFormat = "Button[{0}.{1}] clicked!";
        }

        protected override void OnAttached()
        {
            this.AssociatedObject.Click += new System.Windows.RoutedEventHandler(button_Click);
        }



        protected override void OnDetaching()
        {
            this.AssociatedObject.Click -= new System.Windows.RoutedEventHandler(button_Click);
        }


        void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
                Console.WriteLine(OutputFormat, btn.Content, btn.Tag);
        }




        public string OutputFormat { get; set; }
    }
}

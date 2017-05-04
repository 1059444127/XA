using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace UIH.XA.XSample.Utility
{
    public class VisualElementHepler
    {

        public static T FindVisualChild<T>(DependencyObject depObj, string name) where T : FrameworkElement
        {
            if (depObj != null)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(depObj);
                for (int i = 0; i < childrenCount; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        if (((T)child).Name == name)
                            return (T)child;
                    }

                    T childTemp = FindVisualChild<T>(child, name);
                    if (childTemp != null)
                        return (T)childTemp;
                }
            }
            return null;
        }


        public static IList<T> FindVisualChild<T>(DependencyObject depObj) where T : FrameworkElement
        {
            IList<T> list = new List<T>();
            if (depObj != null)
            {


                int childrenCount = VisualTreeHelper.GetChildrenCount(depObj);
                for (int i = 0; i < childrenCount; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null)
                    {

                        if (child is T)
                            list.Add((T)child);

                        IList<T> clist = FindVisualChild<T>(child);
                        foreach (var c in clist)
                            list.Add(c);
                    }
                }
            }
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Monitor
{

    /// <summary>
    /// This class is used to control effect for control
    /// </summary>
    public class Utilities
    {
        public static string ResourceKey = "";

        public static T FindAncestor<T>(DependencyObject uiElement) where T : DependencyObject
        {
            var iter = uiElement;
            while (iter != null && !(iter is T))
            {
                iter = VisualTreeHelper.GetParent(iter);
            }
            return iter as T;
        }

        public static T FindDescendant<T>(DependencyObject uiElement) where T : DependencyObject
        {
            if (uiElement as T != null)
            {
                return uiElement as T;
            }

            T descendant = null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(uiElement); i++)
            {
                descendant = FindDescendant<T>(VisualTreeHelper.GetChild(uiElement, i));
                if (descendant as T != null) break;
            }
            return descendant;
        }

        public static List<T> GetAllChildsElement<T>(DependencyObject obj) where T : class
        {
            List<T> elements = new List<T>();
            GetChildUiElement(ref elements, obj);
            return elements;
        }

        public static void GetChildUiElement<T>(ref List<T> elements, DependencyObject obj) where T : class
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject currentObj = VisualTreeHelper.GetChild(obj, i);
                if (currentObj is T)
                {
                    elements.Add(currentObj as T);
                }
                GetChildUiElement(ref elements, currentObj);
            }
        }

        public static void CancelVisualEffects<T>(List<T> elements)
        {
             var effectDefinedInResource = Application.Current.Resources[ResourceKey];
             if (effectDefinedInResource == null)
             {
                 return;
             }
            foreach (var item in elements)
            {
                var contorl = item as FrameworkElement;
                if (contorl == null)
                {
                    return;
                }
                contorl.Effect = null; 
            }
        }

        public static void AddVisualEffects<T>(List<T> elements)
        {
            foreach (var item in elements)
            {
                var control = item as FrameworkElement;
                if (control == null)
                {
                    return;
                }
                if (control.Effect == null)
                {
                    var effectDefinedInResource = Application.Current.Resources[ResourceKey];
                    var effectResource = effectDefinedInResource as Effect;
                    if (effectResource == null)
                    {
                        return;
                    }
                    control.Effect = effectResource;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace UIH.XR.Common.Region
{
     public class RegionNameHelper
     {
         public const string RegionNamePropertyName = "RegionName";

         public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached(
             RegionNamePropertyName,
             typeof(string),
             typeof(RegionNameHelper),
             new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, OnRegionNamePropertyChanged)
             );

         public static string GetRegionName(DependencyObject dependencyObject)
         {
             return (string)dependencyObject.GetValue(RegionNameProperty);
         }

         public static void SetRegionName(DependencyObject dependencyObject, string value)
         {
             dependencyObject.SetValue(RegionNameProperty, value);
         }

         private static void OnRegionNamePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
         {
             //Debugger.Launch();

             var regionName = args.NewValue as string;
             if (string.IsNullOrEmpty(regionName))
             {
                //todo:
             }
             if (dependencyObject is ContentControl || dependencyObject is ItemsControl || dependencyObject is TabControl )
             {
                 RegionManager.SetRegionName(dependencyObject, regionName);
             }
             else
             {
                 //todo:
             }
         }
    }
}

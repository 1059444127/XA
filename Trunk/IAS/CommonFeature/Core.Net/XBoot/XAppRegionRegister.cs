//////////////////////////////////////////////////////////////////////////
/// Copyright, (c) Shanghai United Imaging Healthcare Inc., 2011
/// All rights reserved.
///
/// \author Jinyang Li     jinyang.li@united-imaging.com
//
/// \file XAppRegionRegister.cs
///
/// \brief  This Class Generalize the Interface Of IAppRegionManager
///
/// \version 1
/// \date Apr  , 2017
/////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using UIH.XA.Core.Properties;

namespace UIH.XA.Core
{
    public class XAppRegionRegister
    {
        public const string RegionNamePropertyName = "RegionName";

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached(
            RegionNamePropertyName,
            typeof(string),
            typeof(XAppRegionRegister),
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

            var regionName = args.NewValue as string;

            if (string.IsNullOrEmpty(regionName))
            {
                throw new ArgumentNullException("args");
            }

            //todo:目前只支持两种类型
            if (dependencyObject is ContentControl || dependencyObject is TabControl)
            {
                RegionManager.SetRegionName(dependencyObject, regionName);
            }
            else
            {
                //todo:make the message of exception to Resource
                throw new ArgumentException("dependencyObject is not ContentControl", "dependencyObject");
            }
        }
    }
}

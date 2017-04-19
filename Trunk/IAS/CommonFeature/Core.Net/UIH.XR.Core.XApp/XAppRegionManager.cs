//////////////////////////////////////////////////////////////////////////
/// Copyright, (c) Shanghai United Imaging Healthcare Inc., 2011
/// All rights reserved.
///
/// \author Jinyang Li     jinyang.li@united-imaging.com
//
/// \file XAppRegionManager.cs
///
/// \brief  This Class Generalize the Interface Of IAppRegionManager
///
/// \version 1
/// \date Apr  , 2017
/////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace UIH.XR.Core.XApp
{
    [Export(typeof(IAppRegionManager))]
    public class XAppRegionManager : IAppRegionManager
    {
        [Import(typeof(IRegionManager))]
        private IRegionManager _regionManager;

        public const string RegionNamePropertyName = "RegionName";

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached(
            RegionNamePropertyName,
            typeof(string),
            typeof(XAppRegionManager),
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
                //todo:
            }
            if (dependencyObject is ContentControl)
            {
                RegionManager.SetRegionName(dependencyObject, regionName);
            }
            else
            {
                //todo:
            }
        }

        #region Implementation of IAppRegionManager

        public void RegisterViewToRegion(string regionName, Type viewType)
        {
            _regionManager.RegisterViewWithRegion(regionName, viewType);
        }

        public void RegisterViewToRegion(string regionName, Func<object> getViewDelegate)
        {
            _regionManager.RegisterViewWithRegion(regionName, getViewDelegate);
        }

        public void AddViewToRegion(string regionName, object view)
        {
            _regionManager.AddToRegion(regionName, view);
        }

        public void NavigateTo(string regionName, string viewContractName)
        {
            _regionManager.RequestNavigate(regionName,viewContractName);
        }

        #endregion
    }
}

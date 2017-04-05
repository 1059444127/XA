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
using UIH.XR.Common.MVVM;

namespace UIH.XR.Setting.ViewModel
{
    public class SettingItem : NotificationObject
    {

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }
        private string _title;

        /// <summary>
        /// IsEnable
        /// </summary>
        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                _isEnable = value;
                RaisePropertyChanged(() => IsEnable);
            }
        }
        private bool _isEnable;

        /// <summary>
        /// IconSource
        /// </summary>
        public Uri IconSource
        {
            get { return _iconSource; }
            set
            {
                _iconSource = value;
                RaisePropertyChanged(() => IconSource);
            }
        }
        private Uri _iconSource;
    }
}

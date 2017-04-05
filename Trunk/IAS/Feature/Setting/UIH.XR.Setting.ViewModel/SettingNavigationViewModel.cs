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
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using UIH.XR.Core;
using UIH.XR.GlobalParameter;

namespace UIH.XR.Setting.ViewModel
{
    [Export(ComponentContract.SettingNavigationViewModelName, typeof(NotificationObject))]
    public class SettingNavigationViewModel : NotificationObject
    {
        #region Properties

        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// SettingItemCollection
        /// </summary>
        public ObservableCollection<SettingItem> SettingItemCollection { get; set; }

        #endregion

        #region Constructor

        [ImportingConstructor]
        public SettingNavigationViewModel([Import(typeof(ILogger))]ILogger logger)
        {
            Logger = logger;
            Logger.LoggerName = ComponentContract.SettingLogSource;
            Logger.LogUID = ComponentContract.SettingLogUID;

            SettingItemCollection = new ObservableCollection<SettingItem>();
            SettingItemCollection.Add(new SettingItem() { Title = "DESIGN1" });
            SettingItemCollection.Add(new SettingItem() { Title = "DESIGN2" });
            SettingItemCollection.Add(new SettingItem() { Title = "DESIGN3" });
            SettingItemCollection.Add(new SettingItem() { Title = "DESIGN4" });
            SettingItemCollection.Add(new SettingItem() { Title = "DESIGN5" });
            SettingItemCollection.Add(new SettingItem() { Title = "DESIGN6" });


        }

        #endregion
    }
}

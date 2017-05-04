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
using System.Windows;
using UIH.Mcsf.Core;

namespace UIH.XA.Core
{
    public class XApp : Application
    {
        private string _configPath;
        private ICommunicationProxy _communicationProxy;

        public XApp(string configPath, ICommunicationProxy communicationProxy)
        {
            _configPath = configPath;
            _communicationProxy = communicationProxy;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            XBootstrapper boot = new XBootstrapper(_configPath, _communicationProxy);
            boot.Run();
        }
    }
}

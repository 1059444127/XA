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

namespace UIH.XA.MiniBoot
{
    public class MiniApp : Application
    {

        public MiniApp()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MiniBootConfig config = new MiniBootConfig();
            if (config.SetConfig(e.Args))
            {
                MiniBootstrapper boot = new MiniBootstrapper(config);
                boot.Run();
            }
            else
            {
                throw new Exception("启动参数无效");
            }
        }
    }
}

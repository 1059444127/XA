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
using Microsoft.Practices.Prism.MefExtensions;
using System.Windows;
using Microsoft.Practices.Prism.Logging;
using System.Windows.Controls;

namespace UIH.XA.MiniBoot
{
    public class MiniBootstrapper:MefBootstrapper
    {
        private MiniBootConfig _config;

        public MiniBootstrapper(MiniBootConfig config)
        {
            _config = config;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            var miniWindow = this.Shell as Window;
            var miniControl  = this.Container.GetExportedValue<UserControl>(_config.ViewName);
            miniWindow.Content = miniControl;
            miniWindow.Width = _config.WindowWidth;
            miniWindow.Height = _config.WindowHeight;
            miniWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow = miniWindow;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            // UIH.XA.Core
            this.AggregateCatalog.Catalogs.Add(new System.ComponentModel.Composition.Hosting.AssemblyCatalog(System.Reflection.Assembly.Load("UIH.XA.Core")));
            // UIH.XA.MiniBoot
            this.AggregateCatalog.Catalogs.Add(new System.ComponentModel.Composition.Hosting.AssemblyCatalog(System.Reflection.Assembly.Load("UIH.XA.MiniBoot")));

            List<string> assemblyList = _config.AssemblyList;
            foreach (var assemblyName in assemblyList)
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(assemblyName);
                this.AggregateCatalog.Catalogs.Add(new System.ComponentModel.Composition.Hosting.AssemblyCatalog(assembly));
            }

            base.ConfigureAggregateCatalog();
        }

        protected override DependencyObject CreateShell()
        {
            return new MiniWindow();
        }

        protected override ILoggerFacade CreateLogger()
        {
            return base.CreateLogger();
        }


        protected override void InitializeModules()
        {
            base.InitializeModules();
        }

        public override void RegisterDefaultTypesIfMissing()
        {
            base.RegisterDefaultTypesIfMissing();
            
        }
    }
}

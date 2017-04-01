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
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using UIH.Mcsf.Core;
using Microsoft.Practices.Prism.Logging;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;

namespace UIH.XR.Core
{
    public class XBootstrapper : MefBootstrapper
    {
        private string _configPath;
        private XAppConfig _cfg;
        private List<IShell> _shells = new List<IShell>();
        private ICommunicationProxy _communicationProxy;
        private XAppContext _appContext;

        public XBootstrapper(string configPath, ICommunicationProxy communicationProxy)
        {
            _configPath = configPath;
            _communicationProxy = communicationProxy;
        }

        public IAppContext AppContext { get { return _appContext; } }

        protected override void ConfigureAggregateCatalog()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XAppConfig));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetAssembly(this.GetType())));
            using (Stream cfgStream = File.OpenRead(_configPath))
            {
                XAppConfig cfg = xmlSerializer.Deserialize(cfgStream) as XAppConfig;
                foreach (var assemblyName in cfg.Assemblies)
                {
                    Assembly assembly = Assembly.Load(assemblyName);
                    this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(assembly));
                }
                _cfg = cfg;
            }
            base.ConfigureAggregateCatalog();
        }

        protected override DependencyObject CreateShell()
        {
            InitAppContext();
            if (_cfg == null) return null;
            if (_cfg.Shells == null) return null;
            foreach (XShellConfig shellCfg in _cfg.Shells)
            {
                IShellFactory shellFactory = Container.GetExportedValue<IShellFactory>();
                IShell shell = shellFactory.CreateShell(shellCfg.Name);
                _shells.Add(shell);
                if (shellCfg.ShowOnStartup)
                {
                    shell.ShowShell();
                }
                if (!string.IsNullOrWhiteSpace(shellCfg.RootView) && shell is Window)
                {
                    (shell as Window).Content = Container.GetExportedValue<object>(shellCfg.RootView);
                    (shell as Window).Show();
                }
            }
            IShell mainShell = _shells.FirstOrDefault();
            RegisterShellAsRemoteService();
            return mainShell as DependencyObject;
        }

        protected override ILoggerFacade CreateLogger()
        {
            if (_communicationProxy != null)
            {
                return XBootLogger.Instance;
            }
            return base.CreateLogger();
        }

        private void RegisterShellAsRemoteService()
        {
            if (_communicationProxy == null) return;
            foreach (IShell shell in _shells)
            {
                IRemoteMethodInvoker remoteInvoker = Container.GetExportedValue<IRemoteMethodInvoker>();
                remoteInvoker.RegisterServiceObject<IShell>(shell, shell.ShellName);
            }
        }

        private void InitAppContext()
        {
            _appContext = Container.GetExportedValue<IAppContext>() as XAppContext;
            _appContext.DefaultCLRCommunicationProxy = _communicationProxy;
            _appContext.Container = Container;
        }

        public object GetInstance(Type type, string contractName)
        {
            string contract = string.IsNullOrEmpty(contractName) ? AttributedModelServices.GetContractName(type) : contractName;
            return Container.GetExportedValue<object>(contract);
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            RegisterViews();
        }

        private void RegisterViews()
        {
            if (null != _cfg.Views)
            {
                foreach (XViewConfig viewCfg in _cfg.Views)
                {
                    RegisterView(viewCfg);
                } 
            }            
        }

        private void RegisterView(XViewConfig cfg)
        {
            var registry = this.Container.GetExportedValue<IRegionViewRegistry>();
            if (!string.IsNullOrWhiteSpace(cfg.Name) && !string.IsNullOrWhiteSpace(cfg.Region))
            {
                registry.RegisterViewWithRegion(cfg.Region, () =>
                {
                    object view = Container.GetExportedValue<object>(cfg.Name);
                    if (view is Control && !string.IsNullOrWhiteSpace(cfg.DataContext))
                    {
                        (view as Control).DataContext = Container.GetExportedValue<object>(cfg.DataContext);
                    }
                    return view;
                });
            }
        }
    }
}

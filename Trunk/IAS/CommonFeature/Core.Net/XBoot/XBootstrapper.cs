/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.ComponentModel;
using UIH.XA.GlobalParameter;

namespace UIH.XA.Core
{
    public class XBootstrapper : MefBootstrapper
    {
        #region Properties

        private string _configPath;
        private XAppConfig _cfg;
        private ICommunicationProxy _communicationProxy;
        private IAppContext _appContext;
        private IIoC<CompositionContainer> _ioC;
        public IIoC<CompositionContainer> IoC { get { return this._ioC; } }
        public IAppContext AppContext { get { return _appContext; } }

        #endregion


        #region Constructor

        public XBootstrapper(string configPath, ICommunicationProxy communicationProxy)
        {
            _configPath = configPath;
            _communicationProxy = communicationProxy;
        }

        #endregion


        #region Methods

        #region Override base

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

            IShellManager shellManager = Container.GetExportedValue<IShellManager>();
            foreach (XShellConfig shellCfg in _cfg.Shells)
            {
                string shellName = shellCfg.Name;
                IShell shell = Container.GetExportedValue<IShell>(shellName);
                shell.ShellName = shellName;
                shellManager.RegisterShell(shell);
                if (shellCfg.ShowOnStartup)
                {
                    shell.ShowShell();
                }
            }

            return null;
        }

        protected override ILoggerFacade CreateLogger()
        {
            if (_communicationProxy != null)
            {
                return XBootLogger.Instance;
            }
            return base.CreateLogger();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            RegisterViews();
        }

        #endregion


        #region Deriverd

        /// <summary>
        /// Init AppContext
        /// </summary>
        private void InitAppContext()
        {
            _appContext = Container.GetExportedValue<IAppContext>();
            (_appContext as XAppContext).AddObject<ICommunicationProxy>(AppContextObjectName.DefaultCommunicationProxy, this._communicationProxy);


            this._ioC = this.Container.GetExportedValue<IIoC<CompositionContainer>>();
            this._ioC.Container = this.Container;
        }


        /// <summary>
        /// Register View to Region
        /// </summary>
        private void RegisterViews()
        {
            if (null != _cfg.Regions)
            {
                foreach (XRegionConfig regionCfg in _cfg.Regions)
                {
                    RegisterView(regionCfg);
                }
            }
        }

        /// <summary>
        /// Register View to Region
        /// </summary>
        /// <param name="cfg">Region config item</param>
        private void RegisterView(XRegionConfig cfg)
        {
            var registry = this.Container.GetExportedValue<IAppRegionManager>();
            string regionName = cfg.Name;
            if (cfg.IsNavigable)
            {
                foreach (var view in cfg.Views)
                {
                    this.RegisterView(registry, regionName, view.View, view.DataContext);
                }
            }
            else
            {
                this.RegisterView(registry, regionName, cfg.View, cfg.DataContext);
            }
        }

        /// <summary>
        /// Register View to Region
        /// </summary>
        /// <param name="regionRegistry"></param>
        /// <param name="regionName"></param>
        /// <param name="viewName"></param>
        /// <param name="dataContextName"></param>
        private void RegisterView(IAppRegionManager appRegionManager, string regionName, string viewName, string dataContextName)
        {
            if (!string.IsNullOrWhiteSpace(regionName) && !string.IsNullOrWhiteSpace(viewName))
            {
                appRegionManager.RegisterViewToRegion(regionName, () =>
                {
                    var view = Container.GetExportedValue<object>(viewName);
                    if (view is Control && !string.IsNullOrWhiteSpace(dataContextName))
                    {
                        (view as Control).DataContext = Container.GetExportedValue<object>(dataContextName);
                    }
                    return view;
                });
            }
        }
        #endregion

        #endregion
    }
}

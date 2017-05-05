using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using UIH.Mcsf.Viewer;

namespace UIH.XA.ViewerToolKit.ViewerProxy
{
    [ModuleExport(typeof(ViewerDisplayModule))]
    public class ViewerDisplayModule : IModule
    {
        private MedViewerControl _viewerControl;
        private const string VIEWER_REGION_NAME = "ViewerControlRegion";

        public CompositionContainer Container { get; set; }
        public string ConfigPath { get; set; }
        public ViewerProxy ViewerControlProxy { get; set; }

        public void Initialize()
        {
            if (null == Container || string.IsNullOrEmpty(ConfigPath) || null == ViewerControlProxy)
            {
                throw new Exception("ViewerDisplayModule::Initialize() container or config path is null!");
            }

            var registry = this.Container.GetExportedValue<IRegionViewRegistry>();
            registry.RegisterViewWithRegion(VIEWER_REGION_NAME, () =>
                {
                    _viewerControl = new MedViewerControl();
                    ViewerControlProxy.Initialize(_viewerControl, ConfigPath);
                    return _viewerControl;
                });
        }
    }
}

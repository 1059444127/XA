using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using UIH.Mcsf.Core;

namespace UIH.XR.Core
{
    [Export(typeof(IAppContext))]
    class XAppContext : IAppContext
    {
        public CompositionContainer Container { get; set; }

        public object DefaultCLRCommunicationProxy { get; set; }

        public T GetObject<T>(string objectName)
        {
            return Container.GetExportedValue<T>(objectName);
        }

        public T GetObject<T>()
        {
            return Container.GetExportedValue<T>();
        }
    }
}

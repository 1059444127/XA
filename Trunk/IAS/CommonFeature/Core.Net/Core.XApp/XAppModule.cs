using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using System.ComponentModel.Composition;

namespace UIH.XA.Core.XApp
{
    [ModuleExport(typeof(XAppModule))]
    public class XAppModule : IModule
    {
        [Import]
        IAppContext AppContext { get; set; }

        ICommunicator Communicator { get; set; }

        public void Initialize()
        {
            
        }
    }
}

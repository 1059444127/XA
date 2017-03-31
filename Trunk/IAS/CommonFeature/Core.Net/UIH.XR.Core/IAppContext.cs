using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition.Hosting;

namespace UIH.XR.Core
{
    public interface IAppContext
    {
        object DefaultCLRCommunicationProxy { get; }
        CompositionContainer Container { get; }
        T GetObject<T>(string objectName);
        T GetObject<T>();
    }
}

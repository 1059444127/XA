using UIH.XR.Core;
using System.ComponentModel.Composition;
using System;
using UIH.XR.AppManager.Actions;

namespace UIH.XR.AppManager
{
    public class AppManager:IAppManager
    {
        public string CurrentProcedure { get; set; }

        [Import("Transit")]
        private Action<string, object> Transit { get; set; }

        public bool Invoke(string transitionID, object context)
        {
            Transit(transitionID, context);
            return true;
        }
    }
}

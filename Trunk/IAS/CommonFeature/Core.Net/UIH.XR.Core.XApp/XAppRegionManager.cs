using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace UIH.XR.Core.XApp
{
    [Export(typeof(IAppRegionManager))]
	public class XAppRegionManager : IAppRegionManager
	{

        #region [---Fields---]

        [Import(typeof (IRegionManager))] private IRegionManager _regionManager;

        #endregion


        #region Implementation of IAppRegionManager

        public void RegisterViewToRegion(string regionName, Type viewType)
        {
            _regionManager.RegisterViewWithRegion(regionName, viewType);
        }

        public void RegisterViewToRegion(string regionName, Func<object> getViewDelegate)
        {
            _regionManager.RegisterViewWithRegion(regionName, getViewDelegate);
        }

        public void AddViewToRegion(string regionName, object view)
        {
            _regionManager.AddToRegion(regionName, view);
        }

        public void NavigateTo(string regionName, string viewContractName)
        {
            _regionManager.RequestNavigate(regionName, viewContractName);
        }

        #endregion

    }
}

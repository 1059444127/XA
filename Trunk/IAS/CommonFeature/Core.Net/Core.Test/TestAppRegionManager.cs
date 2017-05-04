using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Regions;

namespace UIH.XA.Core.Test
{
   [Export(typeof(IAppRegionManager))]
   public class TestAppRegionManager:IAppRegionManager
   {

       [Import(typeof (IRegionManager))] private IRegionManager _regionManager;


       #region Implementation of IAppRegionManager

       /// <summary>
       /// Register the UI Control to the Region
       /// </summary>
       /// <param name="regionName">placeholder name</param>
       /// <param name="viewType">the UI Control Type</param>
       public void RegisterViewToRegion(string regionName, Type viewType)
       {
           this._regionManager.RegisterViewWithRegion(regionName, viewType);
       }

       /// <summary>
       /// Register the UI Control to the Region
       /// </summary>
       /// <param name="regionName">placeholder name</param>
       /// <param name="getViewDelegate">the delegate which to generate a UI Control Object</param>
       public void RegisterViewToRegion(string regionName, Func<object> getViewDelegate)
       {
           this._regionManager.RegisterViewWithRegion(regionName, getViewDelegate);
       }

       /// <summary>
       /// Add a UI Control Object to the region
       /// </summary>
       /// <param name="regionName">placeholder name</param>
       /// <param name="view">UI Control Object</param>
       public void AddViewToRegion(string regionName, object view)
       {
           this._regionManager.AddToRegion(regionName, view);
       }

       /// <summary>
       /// Navigate to other UI Control
       /// </summary>
       /// <param name="regionName">placeholder name</param>
       /// <param name="viewContractName">Exported UI Control Object Contract Name</param>
       public void NavigateTo(string regionName, string viewContractName)
       {
           this._regionManager.RequestNavigate(regionName,viewContractName);
       }

       #endregion
   }
}

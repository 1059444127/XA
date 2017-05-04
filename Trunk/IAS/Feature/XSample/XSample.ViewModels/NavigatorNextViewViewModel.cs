using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace UIH.XA.XSample.ViewModels
{
    [Export("NavigatorNextViewViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NavigatorNextViewViewModel : NotificationObject
    {
        private string _navigatorContent = "Navigator: I am Next!";

        public string NavigatorContent { get { return this._navigatorContent; } set { this._navigatorContent = value; } }
    }
}

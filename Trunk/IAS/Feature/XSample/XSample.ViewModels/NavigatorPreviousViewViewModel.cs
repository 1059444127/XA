using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace UIH.XA.XSample.ViewModels
{
    [Export("NavigatorPreviousViewViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NavigatorPreviousViewViewModel : NotificationObject
    {
        private string _navigatorContent = "Navigator: I am Previous";

        public string NavigatorContent { get { return this._navigatorContent; } set { this._navigatorContent = value; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel;

namespace UIH.XR.XSample.ViewModels
{
    [Export("HandsomeViewModel")]
    public class HandsomeViewModel : NotificationObject
    {
        public HandsomeViewModel()
        {
            _description = "HANDSOME";
        }
    }
}

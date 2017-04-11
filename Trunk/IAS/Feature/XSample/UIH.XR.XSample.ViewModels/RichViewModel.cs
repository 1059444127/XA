/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Composition;

namespace UIH.XR.XSample.ViewModels
{
    [Export("RichViewModel")]
    public class RichViewModel : NotificationObject
    {
        public RichViewModel()
        {
            _description = "RICH";
        }
    }
}

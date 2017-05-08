using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using UIH.XA.PAUtilityCSharp.Models;

namespace UIH.XA.PAUtilityCSharp.Events
{
    /// <summary>
    /// 自定义事件——PA Study selectchange event
    /// </summary>
    public class PaSelectChangeEvent : CompositePresentationEvent<StudyInfoModel>
    {
    }

    public class AddNewPatientEvent : CompositePresentationEvent<object>
    {
    }
}

using System;

namespace uDicom.WorkQueue.Service.Interface
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class WorkItemKnownTypeAttribute : Attribute
    {
    }
}

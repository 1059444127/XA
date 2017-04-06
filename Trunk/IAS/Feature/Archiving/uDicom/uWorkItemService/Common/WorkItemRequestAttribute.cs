using System;

namespace uDicom.WorkQueue.Service.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class WorkItemRequestAttribute : Attribute
    {
    }
}

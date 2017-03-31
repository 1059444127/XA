/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------

namespace UIH.XR.Core
{
    public interface IWorkflow
    {
        string CurrentProcedure { get; }

        bool Invoke(string procedure, object context);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.XA.PAUtilityCSharp.Enumeration
{
    public enum CommandID
    {
        COMMAND_DATABASE_EVENT = 4101,

    }

    public enum EventID
    {
        PermissionUserLogIn = 10012,
        ServiceHeightAndWeightUnitChanged = 20001,
        ServicePatientRegister = 20008,
        ServiceStudyListHeaderReset = 2006,
        ServiceMedViewerCornerInfoConfigChanged = 20003,
        ServiceMedViewerConfigChanged = 20005,
        ServicePAConfigChanged = 20002,
        StudySendToBeDeleteTable = 10500,
        SystemCommandEventIDSyncTime = 110014,
        WindowsLevelConfig = 20007
    }
}

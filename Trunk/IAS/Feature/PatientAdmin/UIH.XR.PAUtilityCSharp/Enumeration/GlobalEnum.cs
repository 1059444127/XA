using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.XA.PAUtilityCSharp.Enumeration
{
    #region Enum

    public enum PaGridType
    {
        Study,
        Series,
        Image,
        Report,
        Rawdata
    }

    public enum PaGridColSettingType
    {
        Width,
        DisplayIndex
    }

    public enum AgeUnit
    {
        Day,
        Week,
        Month,
        Year
    }

    public enum WeightUnit
    {
        Kg,
        Lb
    }

    public enum HeightUnit
    {
        Cm,
        Inch
    }

    public enum DbItemOperator
    {
        Delete,
        Protect,
        Restore,
    }

    public enum TimeSearch
    {
        Today = 0,
        Yesterday = 1,
        The_Day_Before = 2,
        Since_7_Days = 6,
        Since_30_Days = 29,
        All = 9998,
        Custom = 9999,
    }

    public enum Source
    {
        Localhost,
        DVD,
        USB,
        NetWork,
        RIS,
        Others,
        RecycleBin,
        All,
        RIS_Synchronized,
        External_Storage,
    }

    public enum Protect
    {
        Null,
        P,
        p
    }

    public enum PrintStatus
    {
        UnPrinted = 0,
        Printed = 1,
        PrintFailed = 2
    }

    public enum ExaminedStatus
    {
        Scheduled = 0,
        Examining = 1,
        Suspended = 2,
        Completed = 3,
    }

    public enum ExaminedStatusNotify
    {
        Examining = 8,
        EndExaming = 9,
    }

    public enum ArchiveStatus
    {
        UnArchived = 0,
        Archived = 1
    }

    public enum ConfirmStatus
    {
        Accepted = 0,
        UnAccepted = 1,
        Rejected = 2,
        Deleted = 3,
    }

    public enum ImageSendStatus
    {
        UnSent = 0,
        Sending = 1,
        Sent = 2,
        SendError = 3,
    }

    public enum SendStatus
    {
        UnSent = 0,
        TotalSent = 1,
        PartlySent = 2
    }

    public enum StoredStatus
    {
        UnStored = 0,
        Stored = 1,
        PartlyStored = 2,
    }

    public enum StoredImageStatus
    {
        UnStored = 0,
        Storing = 1,
        Stored = 2,
        StoredError = 3,
    }

    public enum WorkListConnectionStatus
    {
        Normal = 0,
        ContaineeCommunicateFail = 1,
        ConnectRisServerFail = 2,
    }

    public enum RepeatFlag
    {
        Normal = 0,
        NewStudy = 1,
        RepeatStudy = 2
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum LockComment
    {
        StudySplit,
        StudyMerge,
        StudyModify,
        StudyCopy,
        StudyExportToCsv
    }

    public enum StudyOperateFlag
    {
        Add,
        Delete,
        Modify,
        Synchronize,
        None,
    }

    public enum CommandType
    {
        AsyncCommand,
        SyncCommand,
    }
    #endregion
}

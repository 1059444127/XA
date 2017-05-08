using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using UIH.Mcsf.Database;
using UIH.XA.PAUtilityCSharp.Commons;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;
using UIH.XA.PAUtilityCSharp.Models;

namespace UIH.XA.PAUtilityCSharp.Database
{
    public class DataAccessHelper
    {
        public delegate Tout ProcessDBItemDelegate<Tin, Tout>(Tin dbData);

        public static readonly string CallContextName = "PADBWrapper";

        private static readonly DataAccessHelper _default = new DataAccessHelper();

        public static DataAccessHelper Default
        {
            get
            {
                return _default;
            }
        }

        public DBWrapper DBWrapper
        {
            get
            {
                return GetDBInstance();
            }
        }

        private static ProtectHelper _protectHelper = null;

        public ProtectHelper ProtectHelper
        {
            get
            {
                return _protectHelper;
            }
        }

        private static RecycleBinHelper _recycleBin = null;

        public RecycleBinHelper RecycleBin
        {
            get { return _recycleBin; }
        }

        private static DataConverterHelper _dbDataConverter = null;

        public DataConverterHelper DBDataConverter
        {
            get { return _dbDataConverter; }
        }

        public bool IsIncrease;

        static DataAccessHelper()
        {
            InitDataAccess();
        }

        private DataAccessHelper()
        {
            //
        }

        private static void InitDataAccess()
        {
            _protectHelper = new ProtectHelper();
            _dbDataConverter = new DataConverterHelper();
            _recycleBin = new RecycleBinHelper();
        }

        /// <summary>
        /// get Database link
        /// </summary>
        /// <returns>DBWrapper</returns>
        public DBWrapper GetDBInstance()
        {
            var dbWrapper = GetStoredDBWrapper();
            if (dbWrapper == null)
            {
                dbWrapper = CreateDBWrapper();
                StoreDBWrapper(dbWrapper);
            }
            return dbWrapper;
        }

        private DBWrapper CreateDBWrapper()
        {
            DBWrapper dbWrapper = new DBWrapper();
            if (!dbWrapper.Initialize())
            {
                GlobalDefinition.LoggerWrapper.LogDevError("db initialize is failed, it might be caused by db version");
                return null;
            }
            return dbWrapper;
        }

        /// <summary>
        /// StoreDBWrapper
        /// </summary>
        /// <param name="dbWrapper">DBWrapper</param>
        public void StoreDBWrapper(DBWrapper dbWrapper)
        {
            var storedDBWrapper = CallContext.GetData(CallContextName);
            if (storedDBWrapper == null)
            {
                CallContext.SetData(CallContextName, dbWrapper);
            }
        }

        private DBWrapper GetStoredDBWrapper()
        {
            return CallContext.GetData(CallContextName) as DBWrapper;
        }


        #region Lock

        /// <summary>
        /// To determine if this item is Lock
        /// </summary>
        /// <param name="uid">string</param>
        /// <returns>bool:true is lock</returns>
        public bool IsLock(string uid)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public bool IsLock() start");
            try
            {
                bool isLock = GetDBInstance().IsLock(uid);

                if (isLock)
                {
                    List<LockItem> lockItems = GetDBInstance().GetLock(uid);
                    if (lockItems != null)
                    {
                        foreach (var lockItem in lockItems)
                        {
                            GlobalDefinition.LoggerWrapper.LogDevWarning(
                                                                         "the item with uid " + uid + "is locked by "
                                                                         + lockItem.LockOwner);
                        }
                    }
                }

                return isLock;
            }
            catch (System.Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// IsStudyLock
        /// </summary>
        /// <param name="studyUID">string</param>
        /// <returns>bool</returns>
        public bool IsStudyLock(string studyUID)
        {
            if (IsLock(studyUID))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// get all lock from locktable
        /// </summary>
        /// <param name="lockItem">outList<LockItem></param>
        /// <returns>bool</returns>
        public bool IsLockTableHasItem(out List<LockItem> lockItem)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public bool IsLockTableHasItem() start");
            var studylocks = GetDBInstance().GetLock("' or 1=1 or uid='");
            if (studylocks == null
                || studylocks.Count == 0)
            {
                lockItem = null;
                return false;
            }
            lockItem = studylocks;
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public bool IsLockTableHasItem() end");
            return true;
        }

        /// <summary>
        /// GetLock
        /// </summary>
        /// <param name="uid">string</param>
        /// <returns>List<LockItem></returns>
        public List<LockItem> GetLock(string uid)
        {
            return GetDBInstance().GetLock(uid);
        }

        /// <summary>
        /// Lock Study
        /// </summary>
        /// <param name="studyUid">string</param>
        /// <param name="comment">LockComment</param>
        public void LockStudy(
            string studyUid,
            LockComment comment)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public bool LockStudy() start");
            var lockcomment = comment.ToComment();
            if (GetDBInstance().Lock(studyUid, "PA", DBLockLevel.Study, lockcomment))
            {
                GlobalDefinition.LoggerWrapper.LogDevInfo(
                                                          "lock study, uid is " + studyUid + "comment is " + lockcomment);
            }
            else
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning("lock study fail. uid is " + studyUid);
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public bool LockStudy() end");
        }

        /// <summary>
        /// Unlock Study
        /// </summary>
        /// <param name="studyUid">string</param>
        public void UnlockStudy(string studyUid)
        {
            GetDBInstance().UnLock(studyUid, "PA");
        }

        #endregion


        #region studyid

        /// <summary>
        /// Get StudyID FromDatabase And add one to save db
        /// </summary>
        /// <returns>string</returns>
        public string GetStudyIdFromDatabaseAndIncrease()
        {
            string id = string.Empty;
            try
            {
                id = GetStudyIdFromDatabase();

                GetDBInstance().IncreaseMaxStudyID();
            }
            catch (System.Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogException(ex);
            }
            return id;
        }

        /// <summary>
        /// Get StudyID FromDatabase but not save db
        /// </summary>
        /// <returns>string</returns>
        public string GetStudyIdFromDatabaseNotIncrease()
        {
            string id = string.Empty;

            try
            {
                id = GetStudyIdFromDatabase();
                IsIncrease = true;
            }
            catch (System.Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogException(ex);
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string GetStudyIDFromDatabaseNotIncrease() end");
            return id;
        }

        /// <summary>
        /// when PR save success , studyID add one and save db
        /// </summary>
        /// <returns>string</returns>
        public void IncreaseMaxStudyIdToDatabase()
        {
            try
            {
                if (IsIncrease)
                {
                    GetDBInstance().IncreaseMaxStudyID();
                    IsIncrease = false;
                }
            }
            catch (System.Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogException(ex);
            }
        }

        /// <summary>
        /// get studyid from db
        /// </summary>
        /// <returns>string</returns>        
        private string GetStudyIdFromDatabase()
        {
            string id = string.Empty;

            try
            {
                SystemVariable systemVariableTable = GetDBInstance().CreateSystemVariable();
                systemVariableTable.Load();
                id = systemVariableTable.MaxStudyID.ToString();
            }
            catch (System.Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogException(ex);
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string GetStudyIDFromDatabase() end");
            return id;
        }

        #endregion


        #region Get Data from db

        /// <summary>
        /// GetStudyListByFolder
        /// </summary>
        /// <param name="folderName">string</param>
        /// <param name="expStudyCondition">string</param>
        /// <param name="processDelegate">ObservableCollection<StudyInfoModel>></param>
        /// <param name="userName">string</param>
        /// <returns>ObservableCollection<StudyInfoModel></returns>
        public ObservableCollection<StudyInfoModel> GetStudyListByFolder(
            string folderName,
            string expStudyCondition,
            ProcessDBItemDelegate<List<Study>, ObservableCollection<StudyInfoModel>> processDelegate,
            string userName)
        {
            if (null == processDelegate)
            {
                GlobalDefinition.LoggerWrapper.LogDevError("Process Delegate is null.");
                return null;
            }

            try
            {
                try
                {
                    //Get study
                    var dbStudyList = GetDBInstance().GetStudiesByFolder(folderName, expStudyCondition, userName);
                    return processDelegate(dbStudyList);
                }
                catch (Exception ex)
                {
                    GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);

                    return null;
                }
            }
            catch (System.Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogException(ex);
                return null;
            }
        }

        public ObservableCollection<StudyInfoModel> GetStudiesWithPatientByFolder(string folder, string userName)
        {
            //0 Accepted , 1  Un-accept, 2 Reject, 3-deleted
            string expStudyCondition = "StudyConfirmStatus = " + (int)ConfirmStatus.Accepted +
                                       " ORDER BY StudyDate DESC, StudyTime DESC, StudyInstanceUID ASC";
            var dblist = GetDBInstance().GetStudiesByFolderWithPatients(folder, expStudyCondition, userName);
            if (null == dblist)
                return null;
            var backstudys = new ObservableCollection<StudyInfoModel>();
            foreach (var tupleStudyPatient in dblist)
            {
                var study = DataConverterHelper.TupleStudyPatientToStudy(tupleStudyPatient);
                if (null != study)
                {
                    backstudys.Add(study);
                }
            }
            return backstudys;
        }
        #endregion

        #region UpdateStatus
        /// <summary>
        /// UpdateStudyStatus
        /// </summary>
        /// <param name="studyUids">IEnumerable<string></param>
        public void UpdateStudyStatus(IEnumerable<string> studyUids)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public void UpdateStudyStatus() start");
            if (studyUids == null)
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning(
                    "studies in RecycleBinHelper.UpdateStudyStatus(IEnumerable<string> studyUids) is null");
                return;
            }
            var uidList = studyUids.ToList();

            if (!DBWrapper.UpdateStatusByStudyUIDList(uidList, enStatusType.STATUS_ARCHIVING))
            {
                throw new Exception("update study archiving status failed, study uid is " + uidList[0]);
            }
            if (!DBWrapper.UpdateStatusByStudyUIDList(uidList, enStatusType.STATUS_FILMING))
            {
                throw new Exception("update study filming status failed, study uid is " + uidList[0]);
            }
            if (!DBWrapper.UpdateStatusByStudyUIDList(uidList, enStatusType.STATUS_LOCK))
            {
                throw new Exception("update study protect status failed, study uid is " + uidList[0]);
            }

            if (!DBWrapper.UpdateStatusByStudyUIDList(uidList, enStatusType.STATUS_PACSNODES))
            {
                throw new Exception("update study pacsnodes failed, study uid is " + uidList[0]);
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public void UpdateStudyStatus() end");
        }

        public void UpdateStudyProtectStatus(IEnumerable<string> studyUids)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo(" public void UpdateStudyProtectStatus() start");
            if (studyUids == null)
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning(
                    "studyUids in DataAccessHelper.UpdateStudyProtectStatus(IEnumerable<string> studyUids) is null");
                return;
            }

            var uidList = studyUids.ToList();

            if (uidList.Count == 0)
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning(
                    "uid count in DataAccessHelper.UpdateStudyProtectStatus(IEnumerable<string> studyUids) is 0");
                return;
            }

            if (!DBWrapper.UpdateStatusByStudyUIDList(uidList, enStatusType.STATUS_LOCK))
            {
                throw new Exception("update study protect status failed, study uid is " + uidList[0]);
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo(" public void UpdateStudyProtectStatus() end");
        }
        #endregion
    }
}

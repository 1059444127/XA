using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Database;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Commons
{
    public class UIDCreatManager
    {
        private static McsfDatabaseDicomUIDManager _dbUidManager = null;

        public UIDCreatManager()
        {
            _dbUidManager = McsfDatabaseDicomUIDManagerFactory.Instance().CreateUIDManager();
        }

        /// <summary>
        /// CreateStudyUID
        /// </summary>
        /// <returns>string</returns>
        public string CreateStudyUid()
        {

            
            string uid = "";

            if (null != _dbUidManager)
            {
                uid = _dbUidManager.CreateStudyUID("");
            }

            GlobalDefinition.LoggerWrapper.LogDevInfo("Create Study UID: " + uid);
            return uid;
        }

        public string CreatePatientUid()
        {
            string uid = "";

            if (null != _dbUidManager)
            {
                uid = _dbUidManager.CreatePatientUID("");
            }
            GlobalDefinition.LoggerWrapper.LogDevInfo("Create Patient UID: " + uid);
            return uid;
        }
    }
}

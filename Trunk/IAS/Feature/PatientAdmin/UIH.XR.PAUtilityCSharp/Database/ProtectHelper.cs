using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Database;
using UIH.XA.PAUtilityCSharp.Commons;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;
using UIH.XA.PAUtilityCSharp.Models;

namespace UIH.XA.PAUtilityCSharp.Database
{
    public partial class ProtectHelper
    {
        #region Do Update Level Study Status To DB
        private int DoUpdateStudyProtectStatusByUidStrToDb(string uidStr, Protect protect)
        {
            string expCondition = string.Format("StudyInstanceUID IN ({0}) and StudyConfirmStatus='0'", uidStr);
            string expValueList = string.Format("StudyProtect = '{0}'", (int)protect);
            return DataAccessHelper.Default.DBWrapper.UpdateStudyListByCondition(expCondition, expValueList);
        }

        private int DoUpdateSeriesProtectStatusByUidStrToDb(string uidStr, bool isStudyUid, Protect protect)
        {
            string uidName = isStudyUid ? "StudyInstanceUIDFk" : "SeriesInstanceUID";
            string expCondition = string.Format("{0} IN ({1}) and SeriesConfirmStatus='0'", uidName, uidStr);
            string expValueList = string.Format("SeriesProtect = '{0}'", (int)protect);
            return DataAccessHelper.Default.DBWrapper.UpdateSeriesListByCondition(expCondition, expValueList);
        }

        /// <summary>
        /// DoUpdateSeriesProtectStatusWithTrans
        /// </summary>
        /// <param name="seriesList">IList<SeriesInfoModel></param>
        /// <param name="protect">Protect</param>
        /// <returns>int</returns>
        public int DoUpdateSeriesProtectStatusWithTrans(IList<SeriesInfoModel> seriesList, Protect protect)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("[DoUpdateSeriesProtectStatusWithTrans] enter.");
            if (null == seriesList || seriesList.Count == 0)
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning(
                    "[DoUpdateSeriesProtectStatusWithTrans] reports is null/empty!");
                return -1;
            }
            var seriesUidStr = seriesList.CustomJoin("'", "'", ",", r => r.InstanceUID);
            if (string.IsNullOrWhiteSpace(seriesUidStr))
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning(
                    "[DoUpdateSeriesProtectStatusWithTrans] seriesUidStr is null.");
                return -1;
            }
            var back = 0;
            var trans = new Transaction();
            try
            {
                trans.Begin();
                back = DoUpdateSeriesProtectStatusByUidStrToDb(seriesUidStr, false, protect);
                if (back > 0)
                    DataAccessHelper.Default.UpdateStudyProtectStatus(
                        seriesList.Select(c => c.StudyInstanceUidFk).Distinct());
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                GlobalDefinition.LoggerWrapper.LogException("DoUpdateSeriesProtectStatusWithTrans", ex);
                return -1;
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("[DoUpdateSeriesProtectStatusWithTrans] end.");
            return back;
        }

        /// <summary>
        /// DoUpdateStudyProtectStatusWithTrans
        /// </summary>
        /// <param name="studyList">IList<StudyInfoModel></param>
        /// <param name="seriesList">IList<SeriesInfoModel></param>
        /// <param name="reportList">IList<ReportInfoModel></param>
        /// <param name="protect">Protect</param>
        /// <param name="updateStudyAction">outAction</param>
        /// <param name="Dictionary<string">out</param>
        /// <param name="studyRawDataDic">IList<RawData>></param>
        /// <returns>int</returns>
        public int DoUpdateStudyProtectStatusWithTrans(IList<StudyInfoModel> studyList, IList<SeriesInfoModel> seriesList, Protect protect,
            out Action updateStudyAction)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("[DoUpdateStudyProtectStatusWithTrans] enter.");
            updateStudyAction = null;
            if (null == studyList || studyList.Count == 0)
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning(
                    "[DoUpdateStudyProtectStatusWithTrans] reports is null/empty!");
                return -1;
            }
            var studyUidList = studyList.Select(c => c.InstanceUID).ToList();
            var studyUidStr = studyUidList.CustomJoin("'", "'", ",", r => r);
            if (string.IsNullOrWhiteSpace(studyUidStr))
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning(
                    "[DoUpdateStudyProtectStatusWithTrans] studyUidStr is null.");
                return -1;
            }

            var trans = new Transaction();
            try
            {
                trans.Begin();
                DoUpdateSeriesProtectStatusByUidStrToDb(studyUidStr, true, protect);
                if (seriesList != null)
                {
                    updateStudyAction += () =>
                    {
                        foreach (var series in seriesList)
                        {
                            series.Protect = protect;
                        }
                    };
                }
                
                
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                GlobalDefinition.LoggerWrapper.LogException("UpdateStudyProtectStatusWithTrans", ex);
                return -1;
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("[UpdateStudyProtectStatusWithTrans] end.");
            return 1;
        }
        #endregion
    }
}

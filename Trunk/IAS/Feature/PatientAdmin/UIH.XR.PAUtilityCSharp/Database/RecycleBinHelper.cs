using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XA.PAUtilityCSharp.Global;
using UIH.XA.PAUtilityCSharp.Models;

namespace UIH.XA.PAUtilityCSharp.Database
{
    public class RecycleBinHelper : ViewModelBase
    {
        private readonly object _lckIsClearStudies = new object();
        private bool _isClearingStudies = false;
        private bool _hasClearStudiesRequest = false;

        /// <summary>
        /// DeleteSeriesWithTrans
        /// </summary>
        /// <param name="series">IEnumerable<SeriesInfoModel></param>
        /// <returns>bool</returns>
        public bool DeleteSeriesWithTrans(IEnumerable<SeriesInfoModel> series)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public bool DeleteSeriesWithTrans() function start");
            Transaction trans = new Transaction();
            trans.Begin();
            try
            {
                DataAccessHelper.Default.DBWrapper.DeleteSeries(series.Select(o => o.InstanceUID).ToList());
                DataAccessHelper.Default.UpdateStudyStatus(series.Select(o => o.StudyInstanceUidFk).Distinct());
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                trans.Rollback();
                return false;
            }
            trans.Commit();
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public bool DeleteSeriesWithTrans() function end");
            return true;
        }

        public bool DeleteStudiesWithTrans(IEnumerable<StudyInfoModel> studies, string forderName)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public bool DeleteStudiesWithTrans() function start");
            if (studies == null)
            {
                return false;
            }

            var studyList = studies.ToList();
            var studyUids = studyList.Select(o => o.InstanceUID).ToList();
            try
            {

                if (!DataAccessHelper.Default.DBWrapper.UnlinkStudiesFromFolder(studyUids, forderName))
                {
                    GlobalDefinition.LoggerWrapper.LogDevError("从文件夹中删除链接失败");
                    return false;
                }
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                return false;
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public bool DeleteStudiesWithTrans() function end");
            return true;
        }

        /// <summary>
        /// ClearStudiesByDeletedStatus
        /// </summary>
        /// <returns>bool</returns>
        public bool ClearStudiesByDeletedStatus()
        {
            lock (_lckIsClearStudies)
            {
                if (_isClearingStudies)
                {
                    _hasClearStudiesRequest = true;
                    return false;
                }
            }

            bool needDiskClearn = false;

            while (true)
            {
                try
                {
                    lock (_lckIsClearStudies)
                        _isClearingStudies = true;
                    GlobalDefinition.LoggerWrapper.LogDevInfo("[ClearStudiesByDeletedStatus] Clear Begin!");
                    var result = DataAccessHelper.Default.DBWrapper.DeleteStudiesByDeletedStatus();
                    if (result == true)
                        needDiskClearn = true;
                    GlobalDefinition.LoggerWrapper.LogDevInfo("[ClearStudiesByDeletedStatus] Clear End!");
                }
                catch (Exception ex)
                {
                    GlobalDefinition.LoggerWrapper.LogException("ClearStudiesByDeletedStatus", ex);
                }
                finally
                {
                    lock (_lckIsClearStudies)
                        _isClearingStudies = false;
                }

                if (!_hasClearStudiesRequest)
                    break;
                _hasClearStudiesRequest = false;
            }

            return needDiskClearn;
        }

    }
}

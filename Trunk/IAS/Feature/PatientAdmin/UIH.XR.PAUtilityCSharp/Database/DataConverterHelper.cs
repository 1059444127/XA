using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Database;
using UIH.XA.PAUtilityCSharp.Global;
using UIH.XA.PAUtilityCSharp.Models;

namespace UIH.XA.PAUtilityCSharp.Database
{
    public delegate Tout ProcessDbItemDelegate<Tin, Tout>(Tin dbData);   

    public class DataConverterHelper
    {
        private static DBWrapper DbWrapper
        {
            get { return DataAccessHelper.Default.DBWrapper; }
        }

        #region Convert PA Data To DB Data
        //Convert study
        public static Study StudyPa2Db(StudyInfoModel paStudy)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo(" public static Study StudyPA2DB() start");
            if (null == DbWrapper)
            {
                GlobalDefinition.LoggerWrapper.LogDevError("DBWrapper is null");

                return null;
            }

            if (null == paStudy)
            {
                GlobalDefinition.LoggerWrapper.LogDevError("Input PA study is null.");

                return null;
            }

            Study dbStudy = null;

            try
            {
                dbStudy = DbWrapper.GetStudyByStudyInstanceUID(paStudy.InstanceUID);
                if (null == dbStudy)
                {
                    dbStudy = DbWrapper.CreateStudy();
                }
            }
            catch (System.Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);

                return null;
            }

            GlobalDefinition.LoggerWrapper.LogTraceInfo(" public static Study StudyPA2DB() end");
            return dbStudy;
        }

        //Convert patient

        public static Patient PatientPa2Db(PatientInfoModel paPatient)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo(" public static Patient PatientPA2DB() start");
            if (null == DbWrapper)
            {
                GlobalDefinition.LoggerWrapper.LogDevError("DBWrapper is null");

                return null;
            }

            if (null == paPatient)
            {
                GlobalDefinition.LoggerWrapper.LogDevError("Input PA patient is null");

                return null;
            }

            Patient dbPatient = null;

            try
            {
                dbPatient = DbWrapper.GetPatientByPatientUID(paPatient.PatientUid);
                if (null == dbPatient)
                {
                    dbPatient = DbWrapper.CreatePatient();
                    if (null == dbPatient)
                    {
                        GlobalDefinition.LoggerWrapper.LogDevError("Create Patient failed.");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);

                return null;
            }

            return dbPatient;
        }

        //Convert series
        public static Series SeriesPa2Db(SeriesInfoModel paSeries)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public static SeriesInfoModel SeriesPA2DB() start");
            if (DbWrapper == null)
            {
                return null;
            }

            if (paSeries == null)
            {
                return null;
            }

            Series dbSeries;
            try
            {
                dbSeries = DbWrapper.GetSeriesBaseBySeriesInstanceUID(paSeries.InstanceUID) as Series;

                if (dbSeries == null)
                {
                    dbSeries = DbWrapper.CreateSeries();
                    dbSeries.SeriesInstanceUID = paSeries.InstanceUID;
                }
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                return null;
            }

            return dbSeries;
        }
        #endregion

        #region Conver DB Data to PA Data
        //Convert patient
        public static PatientInfoModel PatientDb2Pa(Patient dbPatient)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public static PatientInfoModel PatientDB2PA() start");
            if (null == dbPatient)
            {
                return null;
            }

            PatientInfoModel paPatient = null;
            PatientDb2Pa(dbPatient, out paPatient);
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public static PatientInfoModel PatientDB2PA() end");
            return paPatient;
        }
        public static void PatientDb2Pa(Patient dbPatient, out PatientInfoModel paPatient)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public static void PatientDB2PA() start");
            paPatient = null;

            if (null != dbPatient)
            {
                paPatient = new PatientInfoModel();

                
            }
            else
            {
                GlobalDefinition.LoggerWrapper.LogDevError(" DB Patient is null.");
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public static void PatientDB2PA() end");
        }

        //Convert study
        public static StudyInfoModel StudyDb2Pa(Study dbStudy)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo(" public static StudyInfoModel StudyDB2PA() start");
            if (null == dbStudy)
            {
                return null;
            }

            StudyInfoModel paStudy = null;
            StudyDb2Pa(dbStudy, out paStudy);

            Patient dbPatient = null;
            PatientInfoModel paPatient = null;
            try
            {
                dbPatient = DbWrapper.GetPatientByPatientUID(dbStudy.PatientUIDFk);
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                return null;
            }

            PatientDb2Pa(dbPatient, out paPatient);

            //ProcedureListDB2PA(dbStudy.GetProcedureList());
            paStudy.Patient = paPatient;
            GlobalDefinition.LoggerWrapper.LogTraceInfo(" public static StudyInfoModel StudyDB2PA() end");
            return paStudy;
        }
        public static void StudyDb2Pa(Study dbStudy, out StudyInfoModel studyInfoModel)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo(
                "enter function --- static void StudyDB2PA(Study dbStudy, out StudyInfoModel studyInfoModel)");

            studyInfoModel = null;

            if (null == dbStudy)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(" DB Study is null.");
                return;
            }

            studyInfoModel = new StudyInfoModel();
        }
        /// <summary>
        /// TupleStudyPatientToStudy
        /// </summary>
        /// <param name="tupleStudyPatient">Patient></param>
        /// <returns>StudyInfoModel</returns>
        public static StudyInfoModel TupleStudyPatientToStudy(Tuple<Study, Patient> tupleStudyPatient)
        {
            if (null == tupleStudyPatient)
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning("TupleStudyPatientToStudy::tupleStudyPatient is null");
                return null;
            }
            StudyInfoModel study;
            StudyDb2Pa(tupleStudyPatient.Item1, out study);
            if (null == study)
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning("TupleStudyPatientToStudy::study is null");
                return null;
            }
            PatientInfoModel patient;
            PatientDb2Pa(tupleStudyPatient.Item2, out patient);
            if (null == patient)
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning("TupleStudyPatientToStudy::patient is null");
                return null;
            }
            study.Patient = patient;
            return study;
        }

        #endregion
    }
}

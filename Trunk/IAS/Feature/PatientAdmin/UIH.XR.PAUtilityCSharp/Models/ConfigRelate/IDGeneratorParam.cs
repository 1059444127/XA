using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UIH.XA.PAUtilityCSharp.Commons.CommonConfigRelate;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Models.ConfigRelate
{
    [XmlRoot("IDGenerator")]
    public class IDGeneratorParam
    {
        #region Properties

        //PatientID
        [XmlAttribute]
        public int CurrentMaxIndex
        {
            get;
            set;
        }

        //紧急登记时的PatientID
        [XmlAttribute]
        public int CurrentEmergencyMaxIndex
        {
            get;
            set;
        }

        [XmlAttribute]
        public DateTime CreateIdDate
        {
            get;
            set;
        }

        #endregion


        #region Load And Save

        /// <summary>
        /// Load
        /// </summary>
        public void Load()
        {
            try
            {
                IDGeneratorParam idGenerator = ConfigManager.Load<IDGeneratorParam>(PaConfigType.IdGeneratorParam);
                this.CurrentMaxIndex = idGenerator.CurrentMaxIndex;
                this.CurrentEmergencyMaxIndex = idGenerator.CurrentEmergencyMaxIndex;
                this.CreateIdDate = idGenerator.CreateIdDate;
            }
            catch (System.Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.StackTrace);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {
            try
            {
                ConfigManager.Save(PaConfigType.IdGeneratorParam, this);
            }
            catch (System.Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.StackTrace);
            }
        }

        #endregion


        private struct IDAddedParam
        {
            /// <summary>
            /// true: increase ID; false: increase UID
            /// </summary>
            public bool IsAddId
            {
                get;
                set;
            }
        }

        [XmlIgnore]
        private List<IDAddedParam> _idAddedParamList = new List<IDAddedParam>();

        public string CreatePatientId()
        {
            //  return CreateID("PID-" + DateTime.Now.ToString("yyyyMMdd-") + DateTime.Now.ToString("HHmmss-"));
            return CreateIDByAutoIncrease(
                                          "PID-" + DateTime.Now.ToString("yyyyMMdd-") + DateTime.Now.ToString("HHmmss-"));
        }

        public string CreateIDByAutoIncrease(string prefix)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string CreateIDByAutoIncrease() start");
            string result = CreateId(prefix,true);

            //AddID();
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string CreateIDByAutoIncrease() end");
            return result;
        }

        public string CreateId(string prefix,bool isCreatedId)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string CreateID() start");
            string result = "";

            if (isCreatedId)
            {
                result = prefix + String.Format("{0:D4}", GetRandSuffix(true));
            }
            else
            {
                result = prefix + String.Format("{0:D2}",
                GetRandSuffix( false));
            }
            
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string CreateID() end");
            return result;
        }

        public string CreateEmergencyId(string prefix)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string CreateIDByAutoIncrease() start");
            string result = CreateId(prefix,false);

            //AddID();
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string CreateIDByAutoIncrease() end");
            return result;
        }

        private int GetRandSuffix(bool isCreateId)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("private int GetRandSuffix() start");

            int suffix = 0;

            if (isCreateId)
            {
                suffix = CurrentMaxIndex;
            }
            else
            {
                suffix = CurrentEmergencyMaxIndex;
            }

            IDAddedParam id = new IDAddedParam();
            id.IsAddId = isCreateId;

            _idAddedParamList.Add(id);

            GlobalDefinition.LoggerWrapper.LogTraceInfo("private int GetRandSuffix() end");
            return suffix;
        }

        /// <summary>
        /// ClearParamList
        /// </summary>
        public void ClearParamList()
        {
            _idAddedParamList.Clear();
        }

        public void AddID()
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string AddID () start");
            foreach (IDAddedParam id in _idAddedParamList)
            {
                if (id.IsAddId)
                {
                    if (CurrentMaxIndex == 9999)
                    {
                        CurrentMaxIndex = 0;
                    }
                    else
                    {
                        CurrentMaxIndex++;
                    }
                }
                else
                {
                    if (CurrentEmergencyMaxIndex == 99)
                        //When Click btnEmergency,The last two digits of PatientID should less than 99
                    {
                        CurrentEmergencyMaxIndex = 0;
                    }
                    else
                    {
                        CurrentEmergencyMaxIndex++;
                    }
                }
            }

            ClearParamList();
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string AddID() end");
        }
    }
}

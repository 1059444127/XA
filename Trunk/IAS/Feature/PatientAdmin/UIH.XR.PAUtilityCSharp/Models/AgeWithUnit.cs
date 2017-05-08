using System;
using System.Text.RegularExpressions;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Models
{
    [Serializable]
    public class AgeWithUnit : ViewModelBase
    {
        public AgeWithUnit()
        {

        }

        public AgeWithUnit(int age, AgeUnit ageUnit)
        {
            this.Age = age;
            this.AgeUnit = ageUnit;
        }

        public AgeWithUnit(string str)
        {
            SetAgeAndUnit(str);
        }

        private int? age;

        private AgeUnit? ageUnit;

        public int? Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        public AgeUnit? AgeUnit
        {
            get { return ageUnit; }
            set
            {
                ageUnit = value;
                OnPropertyChanged("AgeUnit");
            }
        }

        public string AgeUnitL10n
        {
            get { return this.AgeUnitToL10nString(); }
        }

        public override string ToString()
        {
            
            if (null != AgeUnit)
            {
                return age.ToString() + AgeUnit.ToString().Substring(0, 1);
            }
            return string.Empty;
        }

        public void SetAgeAndUnit(string str)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public void SetAgeAndUnit() start");
            if (String.IsNullOrEmpty(str) || !Regex.IsMatch(str, "^(?<ageStr>\\d{1,3})(?<unitStr>[^\\d]+)$"))
            {
                GlobalDefinition.LoggerWrapper.LogDevError("Age format is error. input value is : " + str ?? " ");
                return;
            }

            var match = Regex.Match(str, "^(?<ageStr>\\d{1,3})(?<unitStr>[^\\d]+)$");
            var ageStr = match.Groups["ageStr"].Value;
            var unitStr = match.Groups["unitStr"].Value;

            int tempAge;
            if (!int.TryParse(ageStr, out tempAge))
            {
                this.Age = 0;
            }
            this.Age = tempAge;

            switch (unitStr)
            {
                case "Y":
                    this.AgeUnit = Enumeration.AgeUnit.Year;
                    break;
                case "M":
                    this.AgeUnit = Enumeration.AgeUnit.Month;
                    break;
                case "W":
                    this.AgeUnit = Enumeration.AgeUnit.Week;
                    break;
                case "D":
                    this.AgeUnit = Enumeration.AgeUnit.Day;
                    break;
                default:
                    this.AgeUnit = GetAgeUnitFromNls(unitStr);
                    break;
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public void SetAgeAndUnit() end");

        }

        /// <summary>
        /// parse a multi-language string to unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static AgeUnit? GetAgeUnitFromNls(string unit)
        {
            if (unit == GlobalDefinition.ToLocalLanguge("UID_PA_AgeUnit_Year"))
            {
                return Enumeration.AgeUnit.Year;
            }

            if (unit == GlobalDefinition.ToLocalLanguge("UID_PA_AgeUnit_Month"))
            {
                return Enumeration.AgeUnit.Month;
            }

            if (unit == GlobalDefinition.ToLocalLanguge("UID_PA_AgeUnit_Week"))
            {
                return Enumeration.AgeUnit.Week;
            }

            if (unit == GlobalDefinition.ToLocalLanguge("UID_PA_AgeUnit_Day"))
            {
                return Enumeration.AgeUnit.Day;
            }
            return null;
        }

        /// <summary>
        /// age with unit to local language
        /// </summary>
        /// <returns></returns>
        public string ToL10nString()
        {
            if (null != AgeUnit)
            {
                return age.ToString() + AgeUnitToL10nString();
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string ToL10nString() end");
            return string.Empty;
        }

        /// <summary>
        /// unit to local language
        /// </summary>
        /// <returns></returns>
        public string AgeUnitToL10nString()
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string AgeUnitToL10nString() start");
            if (null != AgeUnit)
            {
                return GlobalDefinition.ToLocalLanguge("UID_PA_AgeUnit_" + AgeUnit.ToString());
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public string AgeUnitToL10nString() end");
            return string.Empty;
        }
    }
}

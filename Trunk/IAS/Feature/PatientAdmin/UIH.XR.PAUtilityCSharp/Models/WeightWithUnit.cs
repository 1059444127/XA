using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Models
{
    [Serializable]
    public class WeightWithUnit
    {
        public WeightWithUnit()
        {
        }

        public WeightWithUnit(string str, WeightUnit unit)
        {
            this.Num = str;
            this.Unit = unit;
        }

        public WeightWithUnit(double weight, WeightUnit unit)
        {
            this.Num = weight.ToString();
            this.Unit = unit;
        }

        private string _num = string.Empty;
        private WeightUnit _unit;
        private const double KgToLb = 2.2046226218488;
        private const double LbToKg = 0.45359237;

        public string Num
        {
            get
            {
                var temp = SetPatientWeight(_num);
                if (temp < 0)
                {
                    return string.Empty;
                }
                return _num;
            }
            set { _num = value; }
        }

        public WeightUnit Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        /// <summary>
        /// ToLocalKgWithoutUnitString
        /// </summary>
        /// <returns>string</returns>
        public string ToLocalKgWithoutUnitString()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return string.Empty;
            }

            var temp = SetPatientWeight(this.Num);

            if (this.Unit == WeightUnit.Lb)
            {
                if (Num.EndsWith("."))
                {
                    return Math.Round(temp * LbToKg, 0).ToString(CultureInfo.InvariantCulture) + ".";
                }
                return Math.Round(temp * LbToKg, 2).ToString("0.00");
            }
            else
            {
                if (Num.EndsWith("."))
                {
                    return Math.Round(temp, 0).ToString(CultureInfo.InvariantCulture) + ".";
                }
                if (Num.EndsWith("0"))
                {
                    return Num;
                }
                return Math.Round(temp, 2).ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// ToLocalKgWithUnitString
        /// </summary>
        /// <returns>string</returns>
        public string ToLocalKgWithUnitString()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return string.Empty;
            }

            var temp = SetPatientWeight(this.Num);

            if (this.Unit == WeightUnit.Lb)
            {
                return Math.Round(temp * LbToKg, 2).ToString(CultureInfo.InvariantCulture) +
                       GlobalDefinition.ToLocalLanguge("UID_PR_PatientWeight_kg");
            }
            else
            {
                return Math.Round(temp, 2) + GlobalDefinition.ToLocalLanguge("UID_PR_PatientWeight_kg");
            }
        }

        /// <summary>
        /// ToLocalLbWithUnitString
        /// </summary>
        /// <returns>string</returns>
        public string ToLocalLbWithUnitString()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return string.Empty;
            }

            var temp = SetPatientWeight(this.Num);

            if (this.Unit == WeightUnit.Kg)
            {
                return Math.Round(temp * KgToLb, 2).ToString(CultureInfo.InvariantCulture) +
                       GlobalDefinition.ToLocalLanguge("UID_PR_PatientWeight_lb");
            }
            else
            {
                return Math.Round(temp, 2).ToString(CultureInfo.InvariantCulture) +
                       GlobalDefinition.ToLocalLanguge("UID_PR_PatientWeight_lb");
            }
        }

        /// <summary>
        /// ToLocallbWithoutUnitString
        /// </summary>
        /// <returns>string</returns>
        public string ToLocallbWithoutUnitString()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return string.Empty;
            }

            var temp = SetPatientWeight(this.Num);

            if (this.Unit == WeightUnit.Kg)
            {
                if (Num.EndsWith("."))
                {
                    return Math.Round(temp * KgToLb, 0).ToString(CultureInfo.InvariantCulture) + ".";
                }
                return Math.Round(temp * KgToLb, 2).ToString("0.00");
            }
            else
            {
                if (Num.EndsWith("."))
                {
                    return Math.Round(temp, 0).ToString(CultureInfo.InvariantCulture) + ".";
                }
                if (Num.EndsWith("0"))
                {
                    return Num;
                }
                GlobalDefinition.LoggerWrapper.LogTraceInfo("public string ToLocallbWithoutUnitString() end ");
                return Math.Round(temp, 2).ToString(CultureInfo.InvariantCulture);
            }
        }

        public override string ToString()
        {
            switch (Unit)
            {
                case WeightUnit.Lb:
                    return ToLocalLbWithUnitString();
                case WeightUnit.Kg:
                    return ToLocalKgWithUnitString();
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// ConvertDataToDB
        /// </summary>
        /// <returns>string</returns>
        public string ConvertDataToDB()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return string.Empty;
            }

            var temp = SetPatientWeight(this.Num);

            if (this.Unit == WeightUnit.Lb)
            {
                return (temp * LbToKg).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                return Num;
            }
        }

        /// <summary>
        /// ToKgDoubleNum
        /// </summary>
        /// <returns>double</returns>
        public double ToKgDoubleNum()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return 0;
            }

            var temp = SetPatientWeight(this.Num);

            if (this.Unit == WeightUnit.Lb)
            {
                return temp * LbToKg;
            }
            return temp;
        }

        /// <summary>
        /// ToLbDoubleNum
        /// </summary>
        /// <returns>double</returns>
        public double ToLbDoubleNum()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return 0;
            }

            var temp = SetPatientWeight(this.Num);

            if (this.Unit == WeightUnit.Kg)
            {
                return temp * KgToLb;
            }
            return temp;
        }

        /// <summary>
        /// ToDoubleNum
        /// </summary>
        /// <returns>double</returns>
        public double ToDoubleNum()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return 0;
            }

            var temp = SetPatientWeight(this.Num);
            return temp;
        }

        private double SetPatientWeight(string str)
        {
            double weight;
            var succeed = double.TryParse(str, out weight);
            if (succeed)
            {
                return weight;
            }
            return 0;
        }

        /// <summary>
        /// ConvertToObjWithKg
        /// </summary>
        /// <returns>WeightWithUnit</returns>
        public WeightWithUnit ConvertToObjWithKg()
        {
            if (string.IsNullOrWhiteSpace(Num))
            {
                return new WeightWithUnit(string.Empty, WeightUnit.Kg);
            }
            return new WeightWithUnit(this.ToKgDoubleNum(), WeightUnit.Kg);
        }

        /// <summary>
        /// ConverterToObjWithLb
        /// </summary>
        /// <returns>WeightWithUnit</returns>
        public WeightWithUnit ConverterToObjWithLb()
        {
            if (string.IsNullOrWhiteSpace(Num))
            {
                return new WeightWithUnit(string.Empty, WeightUnit.Lb);
            }
            return new WeightWithUnit(this.ToLbDoubleNum(), WeightUnit.Lb);
        }
    }
}

using System;
using System.Globalization;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Models
{
    [Serializable]
    public class HeightWithUnit
    {
        public HeightWithUnit()
        {

        }

        public HeightWithUnit(string str, HeightUnit height)
        {
            this.Num = str;
            this.Unit = height;

        }

        public HeightWithUnit(double height, HeightUnit unit)
        {
            this.Num = height.ToString();
            this.Unit = unit;
        }

        private string _num = string.Empty;
        private HeightUnit _unit;
        private const double InchToCm = 2.54;
        private const double CmToInch = 0.39370078740157;

        public string Num
        {
            get
            {
                var temp = ConvertPatientHeight(_num);
                if (temp < 0)
                {
                    return string.Empty;
                }
                return _num;
            }
            set { _num = value; }
        }
        public HeightUnit Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public string ToLocalCmWithUnitString()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return string.Empty;
            }

            var temp = ConvertPatientHeight(this.Num);

            if (this.Unit == HeightUnit.Inch)
            {
                return Math.Round(temp * InchToCm, 2).ToString(CultureInfo.InvariantCulture) + GlobalDefinition.ToLocalLanguge("UID_PR_PatientHeight_cm");
            }
            else
            {
                return Math.Round(temp, 2).ToString(CultureInfo.InvariantCulture) + GlobalDefinition.ToLocalLanguge("UID_PR_PatientHeight_cm");
            }

        }

        public string ToLocalCmWithOutUnitString()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return string.Empty;
            }

            var temp = ConvertPatientHeight(this.Num);

            if (this.Unit == HeightUnit.Inch)
            {
                if (Num.EndsWith("."))
                {
                    return Math.Round(temp * InchToCm, 0).ToString(CultureInfo.InvariantCulture) + ".";
                }
                return Math.Round(temp * InchToCm, 2).ToString(CultureInfo.InvariantCulture);
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

        public string ToLocalInchWithUnitString()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return string.Empty;
            }

            var temp = ConvertPatientHeight(this.Num);

            if (this.Unit == HeightUnit.Cm)
            {
                return Math.Round(temp * CmToInch, 2).ToString(CultureInfo.InvariantCulture) + GlobalDefinition.ToLocalLanguge("UID_PR_PatientHeight_inch");
            }
            else
            {
                return Math.Round(temp, 2).ToString(CultureInfo.InvariantCulture) + GlobalDefinition.ToLocalLanguge("UID_PR_PatientHeight_inch");
            }
        }

        public string ToLocalInchWithoutUnitString()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return string.Empty;
            }

            var temp = ConvertPatientHeight(this.Num);

            if (this.Unit == HeightUnit.Cm)
            {
                if (Num.EndsWith("."))
                {
                    return Math.Round(temp * CmToInch, 0).ToString(CultureInfo.InvariantCulture) + ".";
                }
                return Math.Round(temp * CmToInch, 2).ToString(CultureInfo.InvariantCulture);
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

        public override string ToString()
        {
            switch (Unit)
            {
                case HeightUnit.Inch:
                    return ToLocalInchWithUnitString();
                case HeightUnit.Cm:
                    return ToLocalCmWithUnitString();
                default:
                    return string.Empty;
            }
        }

        public string ConvertDataToDB()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return string.Empty;
            }

            var temp = ConvertPatientHeight(this.Num);

            if (this.Unit == HeightUnit.Inch)
            {
                return Math.Round((temp * InchToCm / 100), 6).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                return Math.Round(ConvertPatientHeight(this.Num) / 100, 6).ToString();
            }
        }

        public double ToCmDoubleNum()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return 0;
            }

            var temp = ConvertPatientHeight(this.Num);

            if (this.Unit == HeightUnit.Inch)
            {
                return temp * InchToCm;
            }    
            return temp;
        }

        public double ToInchDoubleNum()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return 0;
            }

            var temp = ConvertPatientHeight(this.Num);

            if (this.Unit == HeightUnit.Cm)
            {
                return temp * CmToInch;
            }
            return temp;
        }

        public double ToDoubleNum()
        {
            if (string.IsNullOrEmpty(this.Num))
            {
                return 0;
            }

            var temp = ConvertPatientHeight(this.Num);
            return temp;
        }

        private double ConvertPatientHeight(string str)
        {
            double height;
            var succeed = double.TryParse(str, out height);
            if (succeed)
            {
                return height;
            }
            return 0;
        }

        public HeightWithUnit ConvertToObjWithCm()
        {
            if (string.IsNullOrWhiteSpace(Num))
            {
                return new HeightWithUnit(string.Empty, HeightUnit.Cm);
            }
            return new HeightWithUnit(ToCmDoubleNum(), HeightUnit.Cm);
        }

        public HeightWithUnit ConvertToObjWithInch()
        {
            if (string.IsNullOrWhiteSpace(Num))
            {
                return new HeightWithUnit(string.Empty, HeightUnit.Inch);
            }
            return new HeightWithUnit(ToInchDoubleNum(), HeightUnit.Inch);
        }
    }
}

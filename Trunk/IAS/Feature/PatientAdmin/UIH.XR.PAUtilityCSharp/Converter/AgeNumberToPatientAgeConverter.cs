using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Models;

namespace UIH.XA.PAUtilityCSharp.Converter
{
    public class AgeNumberToPatientAgeConverter : IValueConverter
    {
        /// <summary>
        /// Convert PatientAge format to age number.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var patientAge = value as AgeWithUnit;

            if (null == patientAge)
            {
                return string.Empty;
            }

            if (patientAge.AgeUnit == null)
            {
                return string.Empty;
            }

            SetMeasurementUnit(patientAge.AgeUnit);

            if (patientAge.Age == null || patientAge.Age.Value == 0)
            {
                return string.Empty;
            }

            return patientAge.Age.Value.ToString();
        }

        /// <summary>
        /// Convert age number to PatientAge format.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }

            if (null == measurementUnit)
            {
                SetMeasurementUnit(AgeUnit.Year);
            }

            string number = value.ToString().Trim();

            int age;
            if (int.TryParse(number, out age))
            {
                return new AgeWithUnit(age, measurementUnit.Value);
            }

            return null;
        }

        private static AgeUnit? measurementUnit;

        private static void SetMeasurementUnit(AgeUnit? ageUnitValue)
        {
            measurementUnit = ageUnitValue;
        }
        /// <summary>
        /// ResetAgeUnit
        /// </summary>
        public static void ResetAgeUnit()
        {
            measurementUnit = null;
        }
    }
}

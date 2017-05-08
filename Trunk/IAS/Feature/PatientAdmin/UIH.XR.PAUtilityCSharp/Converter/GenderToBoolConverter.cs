using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using UIH.XA.PAUtilityCSharp.Enumeration;

namespace UIH.XA.PAUtilityCSharp.Converter
{
    public class GenderToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="value">object</param>
        /// <param name="targetType">Type</param>
        /// <param name="parameter">object</param>
        /// <param name="culture">CultureInfo</param>
        /// <returns>object</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                storedGender = null;
                return false;
            }

            if (!(value is Gender))
            {
                return false;
            }

            if (parameter == null)
            {
                return false;
            }

            storedGender = (Gender)value;
            if (storedGender.ToString() == parameter.ToString())
            {
                return true;
            }


            return false;
        }

        /// <summary>
        /// ConvertBack
        /// </summary>
        /// <param name="value">object</param>
        /// <param name="targetType">Type</param>
        /// <param name="parameter">object</param>
        /// <param name="culture">CultureInfo</param>
        /// <returns>object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return storedGender;
            }

            if (parameter == null)
            {
                return storedGender;
            }

            if ((bool)value)
            {
                switch (parameter.ToString())
                {
                    case "Male":
                        storedGender = Gender.Male;
                        break;
                    case "Female":
                        storedGender = Gender.Female;
                        break;
                    case "Other":
                        storedGender = Gender.Other;
                        break;
                }
            }

            return storedGender;
        }

        private Gender? storedGender = null;
    }
}

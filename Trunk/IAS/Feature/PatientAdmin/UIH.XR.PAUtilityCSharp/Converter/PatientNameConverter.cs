using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace UIH.XA.PAUtilityCSharp.Converter
{
    public class PatientNameConverter:IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (null == value)
            {
                return string.Empty;
            }

            var name = value.ToString();
            return name.Replace("^", ",");
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString()))
            {
                return string.Empty;
            }
            return value.ToString().Replace(",", "^");
        }
    }
}

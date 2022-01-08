using System;
using System.Diagnostics;
using System.Windows.Data;

namespace Homework_13.Service.Converters
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            Debug.WriteLine(value);
            if (value.GetType() == typeof(int)) return value.ToString();
            return "Не интовое значение";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            int result;
            if (int.TryParse(value.ToString(), System.Globalization.NumberStyles.Any,
                         culture, out result))
            {
                return result;
            }
            return value;
        }
    }
}

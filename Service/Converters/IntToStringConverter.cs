using System;
using System.Diagnostics;
using System.Windows.Data;
using System.Globalization;

namespace Homework_13.Service.Converters
{
    /// <summary>
    /// Конвертирует int32 в string
    /// </summary>
    public class IntToStringConverter : IValueConverter
    {
        /// <summary>
        /// конвертация инт в стринг
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(
            object value, 
            Type targetType, 
            object parameter,
            CultureInfo culture)
        {
            Debug.WriteLine(value);
            if (value.GetType() == typeof(int)) return value.ToString();
            return "Не интовое значение";
        }

        /// <summary>
        /// обратная конвертация стринг в инт
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter,
            CultureInfo culture)
        {
            int result;
            if (int.TryParse(value.ToString(), NumberStyles.Any,
                         culture, out result))
            {
                return result;
            }
            return value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13.Service.Converters
{
    class LongToStringConverter
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
            if (value.GetType() == typeof(long)) return value.ToString();
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
            long result;
            if (long.TryParse(value.ToString(), NumberStyles.Any,
                         culture, out result))
            {
                return result;
            }
            return value;
        }
    }
}

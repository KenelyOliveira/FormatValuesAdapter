using System;
using System.Globalization;

namespace FormatExpandoValuesByType.Format.Types
{
    internal class DateTimeFormat : IValueAdapter
    {
        public string FormatValue(dynamic value, string culture)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(culture);
            return ((DateTime)value).ToString(cultureInfo.DateTimeFormat.ShortDatePattern, cultureInfo);
        }
    }
}
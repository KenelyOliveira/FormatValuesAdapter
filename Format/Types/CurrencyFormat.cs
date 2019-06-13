using System.Globalization;

namespace FormatExpandoValuesByType.Format.Types
{
    internal class CurrencyFormat : IValueAdapter
    {
        public string FormatValue(dynamic value, string culture) =>
            ((decimal)value).ToString("C", CultureInfo.GetCultureInfo(culture));
    }
}
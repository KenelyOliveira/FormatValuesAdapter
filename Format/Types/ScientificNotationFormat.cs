namespace FormatExpandoValuesByType.Format.Types
{
    internal class ScientificNotationFormat : IValueAdapter
    {
        public string FormatValue(dynamic value, string culture) =>
            ((double)value).ToString("E2");
    }
}
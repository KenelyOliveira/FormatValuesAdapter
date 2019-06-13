namespace FormatExpandoValuesByType.Format
{
    public interface IValueAdapter
    {
        string FormatValue(dynamic value, string culture);
    }
}
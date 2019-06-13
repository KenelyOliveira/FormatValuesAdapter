namespace FormatExpandoValuesByType.Format
{
    internal interface IValueAdapterProvider
    {
        IValueAdapter GetAdapter<TAdapter>();
    }
}
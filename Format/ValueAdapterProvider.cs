namespace FormatExpandoValuesByType.Format
{
    using System;

    internal class ValueAdapterProvider : IValueAdapterProvider
    {
        private readonly IServiceProvider serviceProvider;

        public ValueAdapterProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IValueAdapter GetAdapter<TAdapter>()
        {
            return (IValueAdapter)this.serviceProvider.GetService(typeof(TAdapter));
        }
    }
}
namespace FormatExpandoValuesByType
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using FormatExpandoValuesByType.Format;

    internal class SalesInvoiceMetadataValueAdapter
    {
        #region Constructor

        private readonly IValueAdapterProvider valueAdapterProvider;

        public SalesInvoiceMetadataValueAdapter(IValueAdapterProvider valueAdapterProvider)
        {
            this.valueAdapterProvider = valueAdapterProvider;
        }

        #endregion Constructor

        private ExpandoObject Metadata { get; set; }

        public static SalesInvoiceMetadataValueAdapter Construct(IValueAdapterProvider valueAdapterProvider)
        {
            return new SalesInvoiceMetadataValueAdapter(valueAdapterProvider);
        }

        public SalesInvoiceMetadataValueAdapter Adapt<TAdpater>(string path, string culture)
        {
            IList<string> propertities = this.GetProperties(path);

            this.RunTransformation<TAdpater>(Metadata, propertities, culture);

            return this;
        }

        public SalesInvoiceMetadataValueAdapter WithData(dynamic metadata)
        {
            this.Metadata = metadata as ExpandoObject;

            return this;
        }

        public void WriteAll()
        {
            foreach (var item in this.Metadata as IDictionary<string, object>)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
        }

        private IList<string> GetProperties(string path)
        {
            return path.Split('.').ToList();
        }

        private void RunTransformation<TAdpater>(IDictionary<string, object> Metadata, IList<string> properties, string culture)
        {
            if (!properties.Any())
            {
                return;
            }

            string property = properties.First();
            properties.RemoveAt(0);

            if (!Metadata.ContainsKey(property))
            {
                return;
            }

            if (!properties.Any())
            {
                Metadata[property] = this.valueAdapterProvider.GetAdapter<TAdpater>().FormatValue(Metadata[property], culture);
                return;
            }

            this.RunTransformation<TAdpater>(Metadata[property] as IDictionary<string, object>, properties, culture);
        }
    }
}
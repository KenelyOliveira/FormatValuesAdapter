using System;
using System.Collections.Generic;
using System.Dynamic;
using FormatExpandoValuesByType.Format;
using FormatExpandoValuesByType.Format.Types;
using Microsoft.Extensions.DependencyInjection;
using DateTimeFormat = FormatExpandoValuesByType.Format.Types.DateTimeFormat;

namespace FormatExpandoValuesByType
{
    public class Program
    {
        private static IDictionary<string, object> Metadata =>
            new Dictionary<string, object>
            {
                { "FallOfBerlinWall", DateTime.Now },
                { "SomeMoney", 12.63000 },
                { "PlanckConstant", 6.626e-34 }
            };

        public static dynamic GetDynamic()
        {
            var ret = new ExpandoObject();
            var dic = (IDictionary<string, object>)ret;

            foreach (var item in Metadata)
            {
                dic.Add(item.Key, item.Value);
            }

            return ret;
        }

        private static void Main(string[] args)
        {
            #region Dependency Injection

            var serviceProvider = new ServiceCollection()
                .AddSingleton(sp => new CurrencyFormat())
                .AddSingleton(sp => new DateTimeFormat())
                .AddSingleton(sp => new ScientificNotationFormat())
                .AddSingleton<IValueAdapterProvider>(
                    sp => new ValueAdapterProvider(
                        sp.GetRequiredService<IServiceProvider>())
                )
                .BuildServiceProvider();

            #endregion Dependency Injection

            var valueAdapterProvider = serviceProvider.GetService<IValueAdapterProvider>();
            var data = GetDynamic();

            SalesInvoiceMetadataValueAdapter
                .Construct(valueAdapterProvider)
                .WithData(data)
                .Adapt<DateTimeFormat>("FallOfBerlinWall", "en-GB")
                .Adapt<CurrencyFormat>("SomeMoney", "en-GB")
                .Adapt<ScientificNotationFormat>("PlanckConstant", "en-GB")
                .WriteAll();

            Console.ReadLine();
        }
    }
}
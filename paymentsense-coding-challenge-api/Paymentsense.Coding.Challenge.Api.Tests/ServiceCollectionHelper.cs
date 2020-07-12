using System;

using Microsoft.Extensions.DependencyInjection;

using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Tests
{
    public static class ServiceCollectionHelper
    {
        private static readonly ServiceCollection _serviceCollection;

        static ServiceCollectionHelper()
        {
            _serviceCollection = new ServiceCollection();

            RegisterDependencies();
        }

        public static IServiceProvider ServiceProvider => _serviceCollection.BuildServiceProvider();

        private static void RegisterDependencies()
        {
            _serviceCollection
                .AddHttpClient()
                .AddSingleton<ICountryService, CountryService>();
        }
    }
}
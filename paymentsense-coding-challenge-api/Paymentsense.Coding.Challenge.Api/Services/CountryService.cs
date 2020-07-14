using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CountryService : ICountryService
    {
        private readonly IApiService _apiService;

        public CountryService(
            IApiService apiService)
        {
            _apiService = apiService;
        }

        public static string Api => "https://restcountries.eu/rest/v2";

        public async Task<IEnumerable<string>> GetNames(
            CancellationToken cancellationToken)
        {
            var countries = await GetCountries(cancellationToken)
                .ConfigureAwait(false);

            return countries
                .Select(country => country.Name);
        }

        private Task<IEnumerable<CountryModel>> GetCountries(CancellationToken cancellationToken)
        {
            var uri = new Uri($"{Api}/all");

            return _apiService.Get<IEnumerable<CountryModel>>(uri, cancellationToken);
        }
    }
}
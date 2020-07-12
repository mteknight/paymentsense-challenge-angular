using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CountryService : ICountryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CountryService(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IEnumerable<string>> GetNames(
            CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://restcountries.eu/rest/v2/all", cancellationToken)
                .ConfigureAwait(false);

            var contentStream = await response.Content.ReadAsStreamAsync()
                .ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var countries = await JsonSerializer.DeserializeAsync<IEnumerable<CountryModel>>(contentStream, options, cancellationToken)
                .ConfigureAwait(false);

            return countries
                .Select(country => country.Name as string);
        }
    }

    public class CountryModel
    {
        public string Name { get; set; }
    }
}
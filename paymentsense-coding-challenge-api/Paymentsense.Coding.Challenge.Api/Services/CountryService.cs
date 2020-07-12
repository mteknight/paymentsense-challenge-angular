using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CountryService : ICountryService
    {
        private const string Api = "https://restcountries.eu/rest/v2";
        private readonly IHttpClientFactory _httpClientFactory;

        public CountryService(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IEnumerable<string>> GetNames(
            CancellationToken cancellationToken)
        {
            var countries = await GetCountries(cancellationToken)
                .ConfigureAwait(false);

            return countries
                .Select(country => country.Name);
        }

        private async Task<IEnumerable<CountryModel>> GetCountries(CancellationToken cancellationToken)
        {
            var uri = new Uri($"{Api}/all");
            var contentStream = await GetFromApi(uri, cancellationToken)
                .ConfigureAwait(false);

            return await Deserialize<IEnumerable<CountryModel>>(contentStream, cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task<Stream> GetFromApi(
            Uri uri,
            CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(uri, cancellationToken)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync()
                .ConfigureAwait(false);
        }

        private static ValueTask<TOut> Deserialize<TOut>(
            Stream stream,
            CancellationToken cancellationToken)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.DeserializeAsync<TOut>(stream, options, cancellationToken);
        }
    }
}
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;

        public ApiService(
            IHttpClientFactory httpClientFactory,
            IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public Task<T> Get<T>(
            Uri uri,
            CancellationToken cancellationToken)
        {
            return _memoryCache.GetOrCreateAsync(uri, entry => CacheFromApi<T>(uri, entry, cancellationToken));
        }

        private async Task<T> CacheFromApi<T>(Uri uri, ICacheEntry entry, CancellationToken cancellationToken)
        {
            var @object = await GetFromApi<T>(uri, cancellationToken)
                .ConfigureAwait(false);

            entry
                .SetSlidingExpiration(TimeSpan.FromHours(1))
                .SetValue(@object);

            return @object;
        }

        private async Task<T> GetFromApi<T>(Uri uri, CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(uri, cancellationToken)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStreamAsync()
                .ConfigureAwait(false);

            return await Deserialize<T>(content, cancellationToken)
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
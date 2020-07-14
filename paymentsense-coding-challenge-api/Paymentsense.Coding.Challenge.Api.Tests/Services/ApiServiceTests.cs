using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;

using FluentAssertions;

using Microsoft.Extensions.Caching.Memory;

using Moq;
using Moq.Protected;

using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;

using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class ApiServiceTests
    {
        private readonly Fixture _fixture;

        public ApiServiceTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Get_OnInvoke_ReturnsData()
        {
            // Arrange
            var countries = _fixture.Create<CountryModel[]>();
            var uri = _fixture.Create<Uri>();

            var response = CreateSuccessfulResponse(countries);
            var httpClientFactory = CreateMockedHttpClientFactory(response);
            var cache = new MemoryCache(new MemoryCacheOptions());
            var sut = new ApiService(httpClientFactory, cache);

            // Act
            var data = await sut.Get<IEnumerable<CountryModel>>(uri, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            data.Should().NotBeNullOrEmpty();
        }

        private static IHttpClientFactory CreateMockedHttpClientFactory(HttpResponseMessage response)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(httpClientFactory => httpClientFactory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            return httpClientFactoryMock.Object;
        }

        private static HttpResponseMessage CreateSuccessfulResponse<T>(T data)
        {
            var json = JsonSerializer.Serialize(data);

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json),
            };
        }
    }
}
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;

using Moq;
using Moq.Protected;

using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;

using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CountryServiceTests
    {
        [Fact]
        public async Task GetNames_OnInvoke_ReturnsCountryNames()
        {
            // Arrange
            var country = new CountryModel { Name = "TestCountry" };
            var responseContent = new[] { country };
            var response = CreateSuccessfulResponse(responseContent);
            var httpClientFactory = CreateMockedHttpClientFactory(response);
            var sut = new CountryService(httpClientFactory);

            // Act
            var countries = await sut.GetNames(CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            countries.Should().NotBeEmpty();
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
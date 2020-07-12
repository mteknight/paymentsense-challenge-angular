using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;

using Paymentsense.Coding.Challenge.Api.Services;

using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CountryServiceTests
    {
        private readonly IServiceProvider _provider;

        public CountryServiceTests()
        {
            _provider = ServiceCollectionHelper.ServiceProvider;
        }

        [Fact]
        public async Task GetNames_OnInvoke_ReturnsCountryNames()
        {
            // Arrange
            var sut = _provider.GetService<ICountryService>();

            // Act
            var countries = await sut.GetNames(CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            countries.Should().NotBeEmpty();
        }
    }
}
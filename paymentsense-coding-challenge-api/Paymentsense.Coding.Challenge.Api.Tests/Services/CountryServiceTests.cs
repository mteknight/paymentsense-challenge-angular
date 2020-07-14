using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;

using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CountryServiceTests
    {
        private readonly Fixture _fixture;

        public CountryServiceTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetNames_OnInvoke_ReturnsCountryNames()
        {
            // Arrange
            var country = _fixture.Create<CountryModel[]>();
            var mockedApiService = new Mock<IApiService>();
            mockedApiService
                .Setup(service => service.Get<IEnumerable<CountryModel>>(It.IsAny<Uri>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(country);

            var sut = new CountryService(mockedApiService.Object);

            // Act
            var countries = await sut.GetNames(CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            countries.Should().NotBeEmpty();
            countries.Should().Contain(country.First().Name);
        }
    }
}
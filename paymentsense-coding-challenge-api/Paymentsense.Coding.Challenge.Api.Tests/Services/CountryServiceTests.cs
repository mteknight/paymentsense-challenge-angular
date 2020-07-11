using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CountryServiceTests
    {
        [Fact]
        public void GetNames_OnInvoke_ReturnsCountryNames()
        {
            // Arrange
            var sut = new CountryService();

            // Act
            var countries = sut.GetNames();

            // Assert
            countries.Should().NotBeEmpty();
        }
    }

    public class CountryService
    {
        public IEnumerable<string> GetNames()
        {
            throw new NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;

using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountryControllerTests
    {
        private readonly CountryController _controller;

        public CountryControllerTests()
        {
            var countries = new[]
            {
                new CountryModel
                {
                    Name = "Test"
                }
            };

            var mockedService = new Mock<ICountryService>();
            mockedService
                .Setup(service => service.GetCountries(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => countries);

            _controller = new CountryController(mockedService.Object);
        }

        [Fact]
        public async Task GetCountryNames_OnInvoke_ReturnsSuccessWithCountryNames()
        {
            var result = (await _controller.Get(CancellationToken.None)).Result as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.As<IEnumerable<CountryModel>>().Should().NotBeEmpty();
        }
    }
}
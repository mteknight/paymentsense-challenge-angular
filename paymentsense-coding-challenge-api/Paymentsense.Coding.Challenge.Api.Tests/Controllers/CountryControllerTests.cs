using System.Collections.Generic;

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
            var mockedService = new Mock<ICountryService>();

            _controller = new CountryController(mockedService.Object);
        }

        [Fact]
        public void Get_OnInvoke_ReturnsExpectedMessage()
        {
            var result = _controller.Get().Result as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().Be("Paymentsense Coding Challenge!");
        }

        [Fact]
        public void GetCountryNames_OnInvoke_ReturnsSuccessWithCountryNames()
        {
            var result = _controller.GetNames().Result as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.As<IEnumerable<CountryModel>>().Should().NotBeEmpty();
        }
    }
}
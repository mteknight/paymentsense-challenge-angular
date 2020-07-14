using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("countries/")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(
            ICountryService countryService)
        {
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        }

        [HttpGet]
        public async Task<ActionResult<CountryModel>> Get(CancellationToken cancellationToken)
        {
            var countries = await _countryService.GetCountries(cancellationToken)
                .ConfigureAwait(false);

            return Ok(countries);
        }
    }
}
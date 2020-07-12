using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(
            ICountryService countryService)
        {
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Paymentsense Coding Challenge!");
        }

        [HttpGet]
        [Route("/names")]
        public async Task<ActionResult<string[]>> GetNames(CancellationToken cancellationToken)
        {
            var countryNames = await _countryService.GetNames(cancellationToken)
                .ConfigureAwait(false);

            return Ok(countryNames);
        }
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryModel>> GetCountries(CancellationToken cancellationToken);
    }
}
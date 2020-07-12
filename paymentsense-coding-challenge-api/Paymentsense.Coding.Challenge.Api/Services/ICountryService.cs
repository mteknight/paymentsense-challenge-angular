using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<string>> GetNames(
            CancellationToken cancellationToken);
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface IApiService
    {
        Task<T> Get<T>(
            Uri uri,
            CancellationToken cancellationToken);
    }
}
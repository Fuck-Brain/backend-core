using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;

namespace ProfileCore.Application.Query
{
    public record OrderGetTotalPriceRequest(Guid Id) : IRequest<ApiResult<decimal>>;
}
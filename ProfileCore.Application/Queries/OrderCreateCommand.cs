using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Query
{
    public record OrderCreateCommand(OrderCreationDto CreationDto) : IRequest<ApiResult>;
}
using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;

namespace ProfileCore.Application.Commands;

public record DeleteCompanyCommand(Guid Id) : IRequest<ApiResult>; // TODO: some other way to return result status
using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;

namespace ProfileCore.Application.Commands;

public record LoginUserCommand(string Email, string Password) : IRequest<ApiResult>; // TODO: return tokens
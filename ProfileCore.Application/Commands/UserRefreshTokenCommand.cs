using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;

namespace ProfileCore.Application.Commands;

public record UserRefreshTokenCommand(string RefreshToken) : IRequest<ApiResult>; // TODO: same thing with tockens
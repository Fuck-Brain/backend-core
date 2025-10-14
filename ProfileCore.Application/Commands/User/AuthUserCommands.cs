using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;

namespace ProfileCore.Application.Commands.User;

public record LoginUserCommand(string Email, string Password) : IRequest<ApiResult>; // TODO: return tokens

public record RegisterUserCommand(string Email, string Password) : IRequest<ApiResult>; // TODO: return tokens

public record UserRefreshTokenCommand(string RefreshToken) : IRequest<ApiResult>; // TODO: same thing with tockens
using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Commands.User;

public record LoginUserCommand(string? Login, string? Email, string Password) : IRequest<TokenDto>;

public record RegisterUserCommand(string Login, string Email, string Password) : IRequest<TokenDto>;

public record UserRefreshTokenCommand(Guid UserId, string RefreshToken) : IRequest<TokenDto>;
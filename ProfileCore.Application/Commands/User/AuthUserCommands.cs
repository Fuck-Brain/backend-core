using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Commands.User;

public record LoginUserCommand(string Email, string Password) : IRequest<TokenDto>;

public record RegisterUserCommand(string Email, string FirstName, string LastName, string FathersName, string Password) : IRequest<TokenDto>;

public record UserRefreshTokenCommand(string RefreshToken) : IRequest<TokenDto>;
using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Commands.User;

public record UpdateUserProfileCommand(Guid Id, UserProfileUpdateDto NewProfile) : IRequest<UserProfileDto>;
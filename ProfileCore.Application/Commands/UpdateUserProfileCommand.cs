using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Commands;

public record UpdateUserProfileCommand(Guid Id, UserProfileDto NewProfile) : IRequest<ApiResult>;
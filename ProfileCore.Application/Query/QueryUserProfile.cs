using MediatR;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Query;

public record QueryUserProfile(Guid Id) : IRequest<UserProfileDto>;
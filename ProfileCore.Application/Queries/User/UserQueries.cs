using MediatR;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Queries.User;

public record QueryUserById(Guid Id) : IRequest<UserProfileDto>;
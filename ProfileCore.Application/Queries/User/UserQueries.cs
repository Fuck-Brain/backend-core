using MediatR;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Queries.User;

public record UserQueries(Guid Id) : IRequest<UserProfileDto>;
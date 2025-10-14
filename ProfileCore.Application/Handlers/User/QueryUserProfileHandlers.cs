using MediatR;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Queries.User;

namespace ProfileCore.Application.Handlers.User;

public class QueryUserProfileHandler : IRequestHandler<UserQueries, UserProfileDto>
{
    public Task<UserProfileDto> Handle(UserQueries request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
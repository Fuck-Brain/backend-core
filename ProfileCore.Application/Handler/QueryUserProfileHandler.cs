using MediatR;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Query;

namespace ProfileCore.Application.Handler;

public class QueryUserProfileHandler : IRequestHandler<QueryUserProfile, UserProfileDto>
{
    public Task<UserProfileDto> Handle(QueryUserProfile request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
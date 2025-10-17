using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Queries.User;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Infrastructure.Database;

namespace ProfileCore.Application.Handlers.User;

public class QueryUserProfileHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<QueryUserById, UserProfileDto>
{
    public async Task<UserProfileDto> Handle(QueryUserById request, CancellationToken cancellationToken)
    {
        var userProfile = await dbContext.UserProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(up => up.Id == request.Id, cancellationToken);
        if (userProfile is null)
            throw new NotFoundException($"Not found user with id: {request.Id}");
        
        return mapper.Map<UserProfileDto>(userProfile);
    }
}
using MediatR;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Queries.User;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Domain.IRepository;

namespace ProfileCore.Application.Handlers.User;

public class QueryUserProfileHandler(IUserRepository userRepository) : IRequestHandler<QueryUserById, UserProfileDto>
{
    public async Task<UserProfileDto> Handle(QueryUserById request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id);
        if (user is null)
            throw new NotFoundException($"Not found user with id: {request.Id}");
        
        return new UserProfileDto(user.FirstName, user.LastName, user.FatherName, "Some user bio", DateTime.MinValue); // TODO: user profile infrastracture
    }
}
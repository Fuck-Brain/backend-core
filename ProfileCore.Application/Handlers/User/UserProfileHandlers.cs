using System.Net;
using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands.User;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Domain.IRepository;

namespace ProfileCore.Application.Handlers.User;

public class UpdateUserProfileHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserProfileCommand>
{
    public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id);
        if (user is null) 
            throw new NotFoundException("Not found user with id: " + request.Id);
        var newProfile = request.NewProfile;
        if (newProfile.Name is not null)
            user.FirstName = newProfile.Name;
        if (newProfile.Surname is not null)
            user.LastName = newProfile.Surname;
        if (newProfile.FathersName is not null)
            user.FatherName = newProfile.FathersName;

        await userRepository.UpdateAsync(user);
    }
}
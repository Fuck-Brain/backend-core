using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileCore.Application.Commands.User;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Infrastructure.Database;

namespace ProfileCore.Application.Handlers.User;

public class UpdateUserProfileHandler(ApplicationDbContext dbContext) : IRequestHandler<UpdateUserProfileCommand>
{
    public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await dbContext.UserProfiles.FirstOrDefaultAsync(up => up.Id == request.Id, cancellationToken);
        if (userProfile is null) 
            throw new NotFoundException("Not found user with id: " + request.Id);
        
        var newProfile = request.NewProfile;
        if (newProfile.Name is not null)
            userProfile.UpdateName(newProfile.Name);
        if (newProfile.Surname is not null)
            userProfile.UpdateSurname(newProfile.Surname);
        if (newProfile.FathersName is not null)
            userProfile.UpdateFatherName(newProfile.FathersName);
        if (newProfile.Bio is not null)
            userProfile.UpdateBio(newProfile.Bio);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
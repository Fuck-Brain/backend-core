using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands.User;

namespace ProfileCore.Application.Handlers.User;

public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommand, ApiResult>
{
    public Task<ApiResult> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
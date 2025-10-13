using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Handler;

public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommand, ApiResult>
{
    public Task<ApiResult> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
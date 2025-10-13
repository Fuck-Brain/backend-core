using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands;

namespace ProfileCore.Application.Handler;

public class UserRefreshTokenHandler : IRequestHandler<UserRefreshTokenCommand, ApiResult>
{
    public Task<ApiResult> Handle(UserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
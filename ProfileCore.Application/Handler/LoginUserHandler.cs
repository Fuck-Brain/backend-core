using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands;

namespace ProfileCore.Application.Handler;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, ApiResult>
{
    public Task<ApiResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
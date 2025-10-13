using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands;

namespace ProfileCore.Application.Handler;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ApiResult>
{
    public Task<ApiResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
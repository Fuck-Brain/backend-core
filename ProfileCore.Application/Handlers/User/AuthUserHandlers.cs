using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands.User;

namespace ProfileCore.Application.Handlers.User;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, ApiResult>
{
    public Task<ApiResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ApiResult>
{
    public Task<ApiResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class UserRefreshTokenHandler : IRequestHandler<UserRefreshTokenCommand, ApiResult>
{
    public Task<ApiResult> Handle(UserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
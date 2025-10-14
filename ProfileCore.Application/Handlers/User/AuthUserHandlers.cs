using MediatR;
using ProfileCore.Application.Commands.User;
using ProfileCore.Application.Dtos;
using ProfileCore.Domain.IRepository;
using ProfileCore.Domain.Entity;
using ProfileCore.Domain.Exceptions;

namespace ProfileCore.Application.Handlers.User;

public class LoginUserHandler(IUserRepository userRepository) : IRequestHandler<LoginUserCommand, TokenDto>
{
    public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // TODO: password check
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            throw new NotFoundException($"User with email: {request.Email} not found");

        return new TokenDto("token", "token"); // TODO: token
    }
}

public class RegisterUserHandler(IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, TokenDto>
{
    public async Task<TokenDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.Entity.User(request.Email, request.Password, request.FirstName, request.LastName, request.FathersName); // TODO: password hash + salt
        await userRepository.AddAsync(user);
        
        return new TokenDto("token", "token"); // TODO: token
    }
}

public class UserRefreshTokenHandler(IUserRepository userRepository) : IRequestHandler<UserRefreshTokenCommand, TokenDto>
{
    public async Task<TokenDto> Handle(UserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // TODO: check password hash + salt
        
        return new TokenDto("token", "token"); // TODO: token
    }
}
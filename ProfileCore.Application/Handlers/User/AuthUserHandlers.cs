using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileCore.Application.Commands.User;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Services.Interfaces;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Infrastructure.Database;

namespace ProfileCore.Application.Handlers.User;

public class LoginUserHandler(ApplicationDbContext dbContext, IJwtTokenService jwtTokenService) : IRequestHandler<LoginUserCommand, TokenDto>
{
    public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Entity.User? user;
        // TODO: password check
        if (request.Login is not null)
        {
            user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken);
            if (user == null)
                throw new NotFoundException($"User with email: {request.Email} not found"); // TODO: common auth error
        } 
        else if (request.Email is not null)
        {
            user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if (user == null)
                throw new NotFoundException($"User with email: {request.Email} not found"); // TODO: common auth error
        }
        else
        {
            throw new ArgumentException("Empty request");
        }
        

		// TODO: Roles
		var jwtToken =  jwtTokenService.GenerateToken(user.Id, user.Email, "User");
		
        return new TokenDto(jwtToken, "stub_refresh_token"); // TODO: token
    }
}

public class RegisterUserHandler(ApplicationDbContext dbContext, IJwtTokenService jwtTokenService) : IRequestHandler<RegisterUserCommand, TokenDto>
{
    public async Task<TokenDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (user is not null)
            throw new ArgumentException(); // TODO: AuthError
        user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken);
        if (user is not null)
            throw new ArgumentException();
        
        
        user = Domain.Entity.User.Create(request.Login, request.Email, request.Password); // TODO: password hash + salt
        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        var token = jwtTokenService.GenerateToken(user.Id, user.Email, "User");
        return new TokenDto(token, "stub_refresh_token"); // TODO: token
    }
}

public class UserRefreshTokenHandler(ApplicationDbContext dbContext, IJwtTokenService jwtTokenService) : IRequestHandler<UserRefreshTokenCommand, TokenDto>
{
    public async Task<TokenDto> Handle(UserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // TODO: check password hash + salt
		var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
		if (user == null)
			throw new NotFoundException($"User with email: {request.UserId} not found");
		
		var jwtToken =  jwtTokenService.GenerateToken(user.Id, user.Email, "User");
        
        return new TokenDto(jwtToken, "stub_refresh_token"); // TODO: token
    }
}
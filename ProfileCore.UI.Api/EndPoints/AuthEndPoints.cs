using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi;
using ProfileCore.Application.Commands.User;
using ProfileCore.Application.Services.Interfaces;
using ProfileCore.UI.Api.DTOs;
using AutoMapper;

namespace ProfileCore.UI.Api.EndPoints;

public static class AuthEndPoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/register", Register);
        group.MapPost("/login", Login);
        group.MapPost("/refresh", Refresh);
        //group.MapGet("/me", Me).RequireAuthorization();;
        return group;
    }

    // -------------------------------
    // POST /register
    // -------------------------------
	[AllowAnonymous]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	private static async Task<IResult> Register(
		RegisterRequest req,
		ILoggerFactory lf,
		IMediator mediator,
		IMapper mapper,
		CancellationToken ct)
	{
		var logger = lf.CreateLogger("Auth");
		if (!TryValidateModel(req, out var errors))
			return Results.ValidationProblem(errors);

		try
		{
			/*var tokenDto = await mediator.Send(
				new RegisterUserCommand(
					req.Email, 
					req.FirstName, 
					req.LastName, 
					req.FatherName,
					req.Password), 
				ct);*/
			var tokenDto = await mediator.Send(mapper.Map<RegisterUserCommand>(req), ct);
			var response = mapper.Map<AuthResponse>(tokenDto);
			
			logger.LogInformation("Stub: User registered {Email}", req.Email);
			return Results.Created("/api/profile/me", response);
		}
		catch (InvalidOperationException ex)
		{
			logger.LogWarning(ex, "Register failed for {Email}", req.Email);
			return Results.Conflict(new { message = "User already exists" });
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Unexpected error during registration");
			return Results.Problem("Internal error");
		}
	}


    // -------------------------------
    // POST /login
    // -------------------------------
	[AllowAnonymous]
	[ProducesResponseType(200)]
	[ProducesResponseType(401)]
    private static async Task<IResult> Login(
        LoginRequest req,
        ILoggerFactory lf,
        IMediator mediator,
		IJwtTokenService jwtService,
		IMapper mapper,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Auth");
        if (!TryValidateModel(req, out var errors))
            return Results.ValidationProblem(errors);

        try
        {
            var tokenDto = await mediator.Send(mapper.Map<LoginUserCommand>(req), ct);
			var response = mapper.Map<AuthResponse>(tokenDto);
			
            logger.LogInformation("Stub: User logged in {Email}", req.Email);
			
            return Results.Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Results.Unauthorized();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Login failed for {Email}", req.Email);
            return Results.Problem("Internal error");
        }
    }

    // -------------------------------
    // POST /refresh
    // -------------------------------
	[ProducesResponseType(200)]
	[ProducesResponseType(401)]
    private static async Task<IResult> Refresh(
        RefreshRequest		req,
        ILoggerFactory		lf,
        IMediator			mediator,
		IMapper				mapper,
        CancellationToken	ct)
    {
        var logger = lf.CreateLogger("Auth");

        try
        {
            var newTokens = await mediator.Send(mapper.Map<UserRefreshTokenCommand>(req), ct);
			var response = mapper.Map<AuthResponse>(newTokens);

			return Results.Ok(response);
        }
        catch (SecurityTokenException)
        {
            return Results.Unauthorized();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Refresh token failed");
            return Results.Problem("Internal error");
        }
    }

    /*// -------------------------------
    // GET /me
    // -------------------------------
	[Authorize]
	[ProducesResponseType(200)]
	[ProducesResponseType(401)]
    private static async Task<IResult> Me(
        ClaimsPrincipal principal,
        ILoggerFactory lf,
        IMediator mediator,
        CancellationToken ct)
	{
        var logger = lf.CreateLogger("Auth");
		var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
		if (uid is null) 
			return Results.Unauthorized();

        try
        {
            var userId = Guid.Parse(uid);

            var profile = await mediator.Send(new GetUserProfileQuery(userId), ct);

            logger.LogInformation("Stub: Fetched profile for {UserId}", userId);
            return Results.Ok(new
            {
                user = new { Id = userId, Email = "stub@example.com", Role = "User" },
                profile = new { FullName = "Stub User" }
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get profile for user {Uid}", uid);
            return Results.Problem("Internal error");
        }
    }*/

	private static bool TryValidateModel<T>(T model, out Dictionary<string, string[]> errors)
	{
		var context = new ValidationContext(model);
		var results = new List<ValidationResult>();
		var isValid = Validator.TryValidateObject(model, context, results, true);

		errors = results
			.GroupBy(r => r.MemberNames.FirstOrDefault() ?? string.Empty)
			.ToDictionary(
				g => g.Key,
				g => g.Select(r => r.ErrorMessage ?? "Invalid").ToArray()
			);

		return isValid;
	}
}

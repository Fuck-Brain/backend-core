using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi;
using ProfileCore.Application.Services.Interfaces;
using ProfileCore.UI.Api.DTOs;

namespace ProfileCore.UI.Api.EndPoints;

public static class AuthEndPoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/register", Register);
        group.MapPost("/login", Login);
        group.MapPost("/refresh", Refresh);
        group.MapGet("/me", Me).RequireAuthorization();;
        return group;
    }

    // -------------------------------
    // POST /register
    // -------------------------------
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	private static async Task<IResult> Register(
		RegisterRequest req,
		ILoggerFactory lf,
		IMediator mediator,
		CancellationToken ct)
	{
		var logger = lf.CreateLogger("Auth");
		if (!TryValidateModel(req, out var errors))
			return Results.ValidationProblem(errors);

		try
		{
			// TODO: var response = await mediator.Send(new RegisterUserCommand(req.Email, req.Password, req.FirstName, req.LastName, req.FatherName, req.Role), ct);

			// ---------- ЗАГЛУШКА ----------
			var userStub = new UserDto(
				Guid.NewGuid(),
				req.Email,
				req.FirstName,
				req.LastName,
				req.FatherName,
				req.Role
			);

			var tokensStub = new AuthResponse(
				"stub_access_token",
				"stub_refresh_token"
			);

			var response = new RegisterResponse(userStub, tokensStub);
			// --------------------------------

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
	[ProducesResponseType(200)]
	[ProducesResponseType(401)]
    private static async Task<IResult> Login(
        LoginRequest req,
        ILoggerFactory lf,
        IMediator mediator,
		IJwtTokenService jwtService,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Auth");
        if (!TryValidateModel(req, out var errors))
            return Results.ValidationProblem(errors);

        try
        {
            // TODO: var token = await mediator.Send(new LoginCommand(req.Email, req.Password), ct);

            logger.LogInformation("Stub: User logged in {Email}", req.Email);
			
			var userId = Guid.NewGuid();
			string email = req.Email;
			string role = "User";

			string accessToken = jwtService.GenerateToken(userId, email, role);
			
            return Results.Ok(new AuthResponse(accessToken, "stub_refresh_token"));
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
        RefreshRequest req,
        ILoggerFactory lf,
        IMediator mediator,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Auth");

        try
        {
            // TODO: var newTokens = await mediator.Send(new RefreshTokenCommand(req.RefreshToken), ct);

            logger.LogInformation("Stub: Token refreshed for refreshToken={Token}", req.RefreshToken);
			return Results.Ok(new AuthResponse("new_access_token", "new_refresh_token"));
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

    // -------------------------------
    // GET /me
    // -------------------------------
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

            // TODO: var profile = await mediator.Send(new GetUserProfileQuery(userId), ct);

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
    }

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

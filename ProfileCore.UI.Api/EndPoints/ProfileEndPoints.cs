using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileCore.UI.Api.DTOs;

namespace ProfileCore.UI.Api.EndPoints;

public static class ProfileEndpoints
{
    public static RouteGroupBuilder MapProfileEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", List).RequireAuthorization("AdminOnly"); // admin only
        group.MapGet("/me", GetMe).RequireAuthorization();
        group.MapPut("/me", UpdateMe).RequireAuthorization();
        group.MapGet("/{id:guid}", GetById).RequireAuthorization("AdminOnly");
        group.MapPost("/", Create).RequireAuthorization("AdminOnly");
        group.MapDelete("/{id:guid}", Delete).RequireAuthorization("AdminOnly");

        return group;
    }

    // -------------------------------
    // GET /profiles
    // -------------------------------
	[Authorize]
	[ProducesResponseType(200)]
    private static async Task<IResult> List(
        IMediator mediator,
        ILoggerFactory lf,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Profiles");

        try
        {
            // TODO: mediator.Send(new GetAllProfilesQuery(), ct);
            logger.LogInformation("Stub: Get all profiles");

            var stubProfiles = new[]
            {
                new ProfileDto(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "Иван",
                    "Иванов",
                    "ivan_ivanov",
                    "Разработчик из Москвы",
                    DateTime.UtcNow
                )
            };

            return Results.Ok(stubProfiles);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to list profiles");
            return Results.Problem("Internal error");
        }
    }

    // -------------------------------
    // GET /profiles/me
    // -------------------------------
	[Authorize]
	[ProducesResponseType(200)]
	[ProducesResponseType(401)]
    private static async Task<IResult> GetMe(
        ClaimsPrincipal principal,
        IMediator mediator,
        ILoggerFactory lf,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Profiles");
        var uid = principal.FindFirstValue("uid");
        if (uid is null)
            return Results.Unauthorized();

        try
        {
            var userId = Guid.Parse(uid);
            // TODO: mediator.Send(new GetProfileByUserIdQuery(userId), ct);
            logger.LogInformation("Stub: Get profile for user {UserId}", userId);

            var stubProfile = new ProfileDto(
                Guid.NewGuid(),
                userId,
                "Пётр",
                "Петров",
                "petya_dev",
                "Backend инженер",
                DateTime.UtcNow
            );

            return Results.Ok(stubProfile);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get profile for user {Uid}", uid);
            return Results.Problem("Internal error");
        }
    }

	[Authorize]
	[ProducesResponseType(200)]
	[ProducesResponseType(401)]
    // -------------------------------
    // PUT /profiles/me
    // -------------------------------
    private static async Task<IResult> UpdateMe(
        ProfileUpdateRequest req,
        ClaimsPrincipal principal,
        IMediator mediator,
        ILoggerFactory lf,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Profiles");
        var uid = principal.FindFirstValue("uid");
        if (uid is null)
            return Results.Unauthorized();

        try
        {
            var userId = Guid.Parse(uid);
            // TODO: mediator.Send(new UpdateProfileCommand(userId, req), ct);
            logger.LogInformation("Stub: Update profile for user {UserId}", userId);

            var updatedProfile = new ProfileDto(
                Guid.NewGuid(),
                userId,
                req.FirstName ?? "Имя",
                req.LastName ?? "Фамилия",
                req.DisplayName ?? "Display Name",
                req.Bio ?? "Обновлённый профиль",
                DateTime.UtcNow
            );

            return Results.Ok(updatedProfile);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update profile for user {Uid}", uid);
            return Results.Problem("Internal error");
        }
    }

	[Authorize]
	[ProducesResponseType(200)]
	[ProducesResponseType(404)]
    // -------------------------------
    // GET /profiles/{id}
    // -------------------------------
    private static async Task<IResult> GetById(
        Guid id,
        IMediator mediator,
        ILoggerFactory lf,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Profiles");

        try
        {
            // TODO: mediator.Send(new GetProfileByIdQuery(id), ct);
            logger.LogInformation("Stub: Get profile by Id {Id}", id);

            var stubProfile = new ProfileDto(
                id,
                Guid.NewGuid(),
                "Сергей",
                "Сергеев",
                "serg_code",
                "Fullstack разработчик",
                DateTime.UtcNow
            );

            return Results.Ok(stubProfile);
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get profile {Id}", id);
            return Results.Problem("Internal error");
        }
    }

	[Authorize]
	[ProducesResponseType(201)]
	[ProducesResponseType(409)]
    // -------------------------------
    // POST /profiles
    // -------------------------------
    private static async Task<IResult> Create(
        ProfileCreateRequest req,
        IMediator mediator,
        ILoggerFactory lf,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Profiles");

        try
        {
            // TODO: mediator.Send(new CreateProfileCommand(req), ct);
            logger.LogInformation("Stub: Create profile for user {UserId}", req.UserId);

            var stubProfile = new ProfileDto(
                Guid.NewGuid(),
                req.UserId,
                req.FirstName ?? "Имя",
                req.LastName ?? "Фамилия",
                req.DisplayName ?? "Display Name",
                req.Bio ?? "Описание профиля",
                DateTime.UtcNow
            );

            var response = new ProfileCreateResponse(stubProfile);
            return Results.Created($"/api/profiles/{stubProfile.Id}", response);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Profile creation failed");
            return Results.Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create profile");
            return Results.Problem("Internal error");
        }
    }

	[Authorize]
	[ProducesResponseType(204)]
	[ProducesResponseType(404)]
    // -------------------------------
    // DELETE /profiles/{id}
    // -------------------------------
    private static async Task<IResult> Delete(
        Guid id,
        IMediator mediator,
        ILoggerFactory lf,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Profiles");

        try
        {
            // TODO: mediator.Send(new DeleteProfileCommand(id), ct);
            logger.LogInformation("Stub: Delete profile {Id}", id);
            return Results.NoContent();
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete profile {Id}", id);
            return Results.Problem("Internal error");
        }
    }
}

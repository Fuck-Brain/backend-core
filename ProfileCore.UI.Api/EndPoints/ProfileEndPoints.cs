using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileCore.Application.Commands.User;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Queries.User;
using ProfileCore.UI.Api.DTOs;

namespace ProfileCore.UI.Api.EndPoints;

public static class ProfileEndpoints
{
    public static RouteGroupBuilder MapProfileEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/me", GetMe).RequireAuthorization();
        group.MapPut("/me", UpdateMe).RequireAuthorization();

        return group;
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
		IMapper mapper,
        CancellationToken ct)
    {
        var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uid is null)
            return Results.Unauthorized();

		var userId = Guid.Parse(uid);
		
		var updatedProfile = await mediator.Send(new QueryUserById(userId), ct);

		var updatedProfileDto = mapper.Map<ProfileDto>(updatedProfile);

		return Results.Ok(updatedProfileDto);
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
		IMapper mapper,
        CancellationToken ct)
    {
        var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uid is null)
            return Results.Unauthorized();

		var userId = Guid.Parse(uid);
		var updatedProfile = await mediator.Send(new UpdateUserProfileCommand(userId, mapper.Map<ProfileCore.Application.Dtos.UserProfileUpdateDto>(req)), ct);
		
		return Results.Ok(mapper.Map<ProfileDto>(updatedProfile));
    }
}

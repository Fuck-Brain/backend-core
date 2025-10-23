using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileCore.UI.Api.DTOs;
using AutoMapper;
using ProfileCore.Application.Commands.Company;
using ProfileCore.Application.Queries.Company;

namespace ProfileCore.UI.Api.EndPoints;

public static class PluginEndpoints
{
    public static RouteGroupBuilder MapCompanyEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", List).RequireAuthorization();
        group.MapGet("/{id:guid}", GetById).RequireAuthorization();

        return group;
    }

    // -------------------------------
    // GET /plugins
    // -------------------------------
    private static async Task<IResult> List(
        IMediator mediator,
		ClaimsPrincipal principal,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
		var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
		if (uid is null)
			return Results.Unauthorized();

		//var companies = await mediator.Send(new QueryAllUsersCompanies(uid), ct);

		return Results.Ok();
    }

    // -------------------------------
    // GET /plugins
    // -------------------------------
    private static async Task<IResult> GetById(
        Guid id,
		ClaimsPrincipal principal,
        IMediator mediator,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
		var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
		if (uid is null)
			return Results.Unauthorized();

		//var company = await mediator.Send(new QueryCompanyById(id, uid), ct);

		return Results.Ok();
    }
}

using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileCore.Application.Commands.Plugin;
using ProfileCore.Application.Queries.Plugin;
using ProfileCore.UI.Api.DTOs;
using AutoMapper;

namespace ProfileCore.UI.Api.EndPoints;

public static class PluginEndpoints
{
    public static RouteGroupBuilder MapPluginEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllPlugins).RequireAuthorization();
        group.MapGet("/company/{id:guid}", GetCompanyPlugins).RequireAuthorization();
        group.MapPost("/", AddPlugin).RequireAuthorization();
        group.MapDelete("/{id:guid}", DeletePlugin).RequireAuthorization();
        group.MapPost("/company", AddPluginToCompany).RequireAuthorization();
        group.MapDelete("/company", RemovePluginFromCompany).RequireAuthorization();

        return group;
    }

    // -------------------------------
    // GET /plugins
    // -------------------------------
    private static async Task<IResult> GetAllPlugins(
        IMediator mediator,
        ClaimsPrincipal principal,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
        var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uid is null)
            return Results.Unauthorized();

        var plugins = await mediator.Send(new QueryAllPlugins(), ct);

        return Results.Ok(plugins);
    }

    // -------------------------------
    // GET /plugins/company/{id}
    // -------------------------------
    private static async Task<IResult> GetCompanyPlugins(
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

        var userId = Guid.Parse(uid);
        var plugins = await mediator.Send(new QueryCompanyPlugins(id, userId), ct);

        return Results.Ok(plugins);
    }

    // -------------------------------
    // POST /plugins
    // -------------------------------
    private static async Task<IResult> AddPlugin(
        PluginCreateRequest req,
        ClaimsPrincipal principal,
        IMediator mediator,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
        var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uid is null)
            return Results.Unauthorized();

        var plugin = await mediator.Send(new AddPluginCommand(req.Name, req.Description), ct);

        return Results.Created($"/api/plugins/{plugin.Id}", plugin);
    }

    // -------------------------------
    // DELETE /plugins/{id}
    // -------------------------------
    private static async Task<IResult> DeletePlugin(
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

        await mediator.Send(new DeletePluginCommand(id), ct);

        return Results.NoContent();
    }

    // -------------------------------
    // POST /plugins/company
    // -------------------------------
    private static async Task<IResult> AddPluginToCompany(
        PluginCompanyRequest req,
        ClaimsPrincipal principal,
        IMediator mediator,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
        var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uid is null)
            return Results.Unauthorized();

        await mediator.Send(new AddCompanyPluginCommand(req.CompanyId, req.PluginId), ct);

        return Results.Ok();
    }

    // -------------------------------
    // DELETE /plugins/company
    // -------------------------------
    private static async Task<IResult> RemovePluginFromCompany(
		[FromBody] PluginCompanyRequest req,
        ClaimsPrincipal principal,
        IMediator mediator,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
        var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uid is null)
            return Results.Unauthorized();

        await mediator.Send(new DeleteCompanyPluginCommand(req.CompanyId, req.PluginId), ct);

        return Results.NoContent();
    }
}
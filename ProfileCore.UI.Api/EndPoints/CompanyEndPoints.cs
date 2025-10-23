using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileCore.UI.Api.DTOs;
using AutoMapper;
using ProfileCore.Application.Commands.Company;
using ProfileCore.Application.Queries.Company;

namespace ProfileCore.UI.Api.EndPoints;

public static class CompanyEndpoints
{
    public static RouteGroupBuilder MapCompanyEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", List).RequireAuthorization();
        group.MapGet("/{id:guid}", GetById).RequireAuthorization();
        group.MapPost("/", Create).RequireAuthorization();
        group.MapPut("/", Update).RequireAuthorization();
        group.MapDelete("/{id:guid}", Delete).RequireAuthorization();

        return group;
    }

    // -------------------------------
    // GET /companies
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

		var companies = await mediator.Send(new QueryAllUsersCompanies(new Guid(uid)), ct);

		return Results.Ok(mapper.Map<List<ProfileCore.UI.Api.DTOs.CompanyDto>>(companies));
    }

    // -------------------------------
    // GET /companies/{id}
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

		var company = await mediator.Send(new QueryCompanyById(id, new Guid(uid)), ct);

		return Results.Ok(mapper.Map<DTOs.CompanyDto>(company));
    }

    // -------------------------------
    // POST /companies
    // -------------------------------
    private static async Task<IResult> Create(
        CompanyCreateRequest req,
        ClaimsPrincipal principal,
        IMediator mediator,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
        var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uid is null)
            return Results.Unauthorized();

		var company = await mediator.Send(new CreateCompanyCommand(req.Name, new Guid(uid)), ct);

		var response = new CompanyCreateResponse(mapper.Map<DTOs.CompanyDto>(company));
		return Results.Created($"/api/companies/{response.Company.Id}", response);   
    }

    // -------------------------------
    // PUT /companies/{id}
    // -------------------------------
    private static async Task<IResult> Update(
        CompanyUpdateRequest req,
        ClaimsPrincipal principal,
        IMediator mediator,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
        var uid = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uid is null)
            return Results.Unauthorized();

		var company = await mediator.Send(new UpdateCompanyCommand(req.Id, req.Name), ct);

		return Results.Ok(mapper.Map<DTOs.CompanyDto>(company));
    }

    // -------------------------------
    // DELETE /companies/{id}
    // -------------------------------
    private static async Task<IResult> Delete(
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

		await mediator.Send(new DeleteCompanyCommand(id), ct);
		
		return Results.NoContent();
    }
}

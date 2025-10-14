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
        group.MapGet("/", List);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete).RequireAuthorization("AdminOnly");

        return group;
    }

    // -------------------------------
    // GET /companies
    // -------------------------------
    private static async Task<IResult> List(
        IMediator mediator,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Companies");

        try
        {
            var companies = await mediator.Send(new QueryAllCompanies(), ct);
            logger.LogInformation("Stub: Get all companies");

            return Results.Ok(mapper.Map<List<ProfileCore.UI.Api.DTOs.CompanyDto>>(companies));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get companies");
            return Results.Problem("Internal error");
        }
    }

    // -------------------------------
    // GET /companies/{id}
    // -------------------------------
    private static async Task<IResult> GetById(
        Guid id,
        IMediator mediator,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Companies");

        try
        {
            var company = await mediator.Send(new QueryCompanyById(id), ct);
            logger.LogInformation("Get company {Id}", id);

            return Results.Ok(mapper.Map<DTOs.CompanyDto>(company));
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get company {Id}", id);
            return Results.Problem("Internal error");
        }
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
        var logger = lf.CreateLogger("Companies");

        var uid = principal.FindFirstValue("uid");
        if (uid is null)
            return Results.Unauthorized();

        try
        {
            var company = await mediator.Send(new CreateCompanyCommand(req.Name, req.OwnerId), ct);
            logger.LogInformation("Stub: Create company {Name}", req.Name);
            

            var response = new CompanyCreateResponse(mapper.Map<DTOs.CompanyDto>(company));
            return Results.Created($"/api/companies/{response.Company.Id}", response);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Company creation failed for {Name}", req.Name);
            return Results.Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create company");
            return Results.Problem("Internal error");
        }
    }

    // -------------------------------
    // PUT /companies/{id}
    // -------------------------------
    private static async Task<IResult> Update(
        Guid id,
        CompanyUpdateRequest req,
        ClaimsPrincipal principal,
        IMediator mediator,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Companies");
        var uid = principal.FindFirstValue("uid");
        if (uid is null)
            return Results.Unauthorized();

        try
        {
            var company = await mediator.Send(new UpdateCompanyCommand(id, req.Name), ct);
            logger.LogInformation("Stub: Update company {Id}", id);

            return Results.Ok(mapper.Map<DTOs.CompanyDto>(company));
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update company {Id}", id);
            return Results.Problem("Internal error");
        }
    }

    // -------------------------------
    // DELETE /companies/{id}
    // -------------------------------
    private static async Task<IResult> Delete(
        Guid id,
        IMediator mediator,
        ILoggerFactory lf,
        IMapper mapper,
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Companies");

        try
        {
            await mediator.Send(new DeleteCompanyCommand(id), ct);
            logger.LogInformation("Stub: Delete company {Id}", id);
            return Results.NoContent();
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete company {Id}", id);
            return Results.Problem("Internal error");
        }
    }
}

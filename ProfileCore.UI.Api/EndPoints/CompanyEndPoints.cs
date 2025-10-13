using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileCore.UI.Api.DTOs;

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
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Companies");

        try
        {
            // TODO: mediator.Send(new GetAllCompaniesQuery(), ct);
            logger.LogInformation("Stub: Get all companies");

            var stub = new[]
            {
                new CompanyDto(
                    Guid.NewGuid(),
                    "TechCore",
                    Guid.NewGuid(),
                    "Иван Иванов",
                    new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                    new List<Guid> { Guid.NewGuid() }
                )
            };

            return Results.Ok(stub);
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
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Companies");

        try
        {
            // TODO: mediator.Send(new GetCompanyByIdQuery(id), ct);
            logger.LogInformation("Stub: Get company {Id}", id);

            var stub = new CompanyDto(
                id,
                "DevTools Ltd",
                Guid.NewGuid(),
                "Пётр Петров",
                new List<Guid> { Guid.NewGuid() },
                new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            );

            return Results.Ok(stub);
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
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Companies");

        var uid = principal.FindFirstValue("uid");
        if (uid is null)
            return Results.Unauthorized();

        try
        {
            // TODO: mediator.Send(new CreateCompanyCommand(req), ct);
            logger.LogInformation("Stub: Create company {Name}", req.Name);

            var stubCompany = new CompanyDto(
                Guid.NewGuid(),
                req.Name,
                req.OwnerId,
                "Stub Owner",
                new List<Guid>(),
                new List<Guid>()
            );

            var response = new CompanyCreateResponse(stubCompany);
            return Results.Created($"/api/companies/{stubCompany.Id}", response);
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
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Companies");
        var uid = principal.FindFirstValue("uid");
        if (uid is null)
            return Results.Unauthorized();

        try
        {
            // TODO: mediator.Send(new UpdateCompanyCommand(id, req), ct);
            logger.LogInformation("Stub: Update company {Id}", id);

            var updated = new CompanyDto(
                id,
                req.Name ?? "Updated Company",
                Guid.Parse(uid),
                "Updated Owner",
                new List<Guid> { Guid.NewGuid() },
                new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            );

            return Results.Ok(updated);
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
        CancellationToken ct)
    {
        var logger = lf.CreateLogger("Companies");

        try
        {
            // TODO: mediator.Send(new DeleteCompanyCommand(id), ct);
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

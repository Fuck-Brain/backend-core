using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Commands.Company;

public record CreateCompanyCommand(string Name, Guid OwnerId) : IRequest<CompanyDto>;

public record DeleteCompanyCommand(Guid Id) : IRequest<ApiResult>; // TODO: some other way to return result status

public record UpdateCompanyCommand(Guid Id, string Name) : IRequest<CompanyDto>;
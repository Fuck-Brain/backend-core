using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Commands;

public record UpdateCompanyCommand(Guid Id, string Name) : IRequest<CompanyDto>;
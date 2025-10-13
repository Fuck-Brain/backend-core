using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Dtos;
using ProfileCore.Domain.Aggregate;

namespace ProfileCore.Application.Commands;

public record CreateCompanyCommand(string Name, Guid OwnerId) : IRequest<CompanyDto>;
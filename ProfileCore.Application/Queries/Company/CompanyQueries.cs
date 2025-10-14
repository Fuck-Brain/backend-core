using MediatR;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Queries.Company;

public record QueryCompanyById(Guid Id) : IRequest<CompanyDto>;

public record QueryAllCompanies() : IRequest<List<CompanyDto>>;
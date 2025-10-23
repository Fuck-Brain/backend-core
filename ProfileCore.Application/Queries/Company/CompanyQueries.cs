using MediatR;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Queries.Company;

public record QueryCompanyById(Guid Id, Guid UserId) : IRequest<CompanyDto>;

public record QueryAllUsersCompanies(Guid UserId) : IRequest<List<CompanyDto>>;
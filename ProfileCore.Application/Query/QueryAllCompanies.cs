using MediatR;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Query;

public record QueryAllCompanies() : IRequest<List<CompanyDto>>;
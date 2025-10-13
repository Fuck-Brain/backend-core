using MediatR;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Query;

public record QueryCompanyById(Guid Id) : IRequest<CompanyDto>;
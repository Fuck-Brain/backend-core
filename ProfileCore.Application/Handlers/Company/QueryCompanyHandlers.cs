using MediatR;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Queries.Company;

namespace ProfileCore.Application.Handlers.Company;

public class QueryAllCompaniesHandler : IRequestHandler<QueryAllCompanies, List<CompanyDto>>
{
    public Task<List<CompanyDto>> Handle(QueryAllCompanies request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class QueryCompanyByIdHandler : IRequestHandler<QueryCompanyById, CompanyDto>
{
    public Task<CompanyDto> Handle(QueryCompanyById request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using MediatR;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Query;

namespace ProfileCore.Application.Handler;

public class QueryAllCompaniesHandler : IRequestHandler<QueryAllCompanies, List<CompanyDto>>
{
    public Task<List<CompanyDto>> Handle(QueryAllCompanies request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
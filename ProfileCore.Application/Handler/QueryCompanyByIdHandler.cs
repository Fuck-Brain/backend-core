using MediatR;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Query;

namespace ProfileCore.Application.Handler;

public class QueryCompanyByIdHandler : IRequestHandler<QueryCompanyById, CompanyDto>
{
    public Task<CompanyDto> Handle(QueryCompanyById request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Queries.Company;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Infrastructure.Database;

namespace ProfileCore.Application.Handlers.Company;

public class QueryAllCompaniesHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<QueryAllCompanies, List<CompanyDto>>
{
    public async Task<List<CompanyDto>> Handle(QueryAllCompanies request, CancellationToken cancellationToken)
    {
        return mapper.Map<List<CompanyDto>>(
            await dbContext.Companies
                .AsNoTracking()
                .ToListAsync(cancellationToken)
            );
    }
}

public class QueryCompanyByIdHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<QueryCompanyById, CompanyDto>
{
    public async Task<CompanyDto> Handle(QueryCompanyById request, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (company is null)
            throw new NotFoundException($"Not found company with id: {request.Id}");
        return mapper.Map<CompanyDto>(company);
    }
}
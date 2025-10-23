using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Queries.Company;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Infrastructure.Database;

namespace ProfileCore.Application.Handlers.Company;

public class QueryAllUsersCompaniesHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<QueryAllUsersCompanies, List<CompanyDto>>
{
    public async Task<List<CompanyDto>> Handle(QueryAllUsersCompanies request, CancellationToken cancellationToken)
    {
        return mapper.Map<List<CompanyDto>>(
            await dbContext.Companies
                .AsNoTracking()
                .Where(c => c.Employees.Any(e => e.UserId == request.UserId))
                .ToListAsync(cancellationToken)
            );
    }
}

public class QueryCompanyByIdHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<QueryCompanyById, CompanyDto>
{
    public async Task<CompanyDto> Handle(QueryCompanyById request, CancellationToken cancellationToken)
    {
        var user = await  dbContext.Users.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.UserId, cancellationToken);
        if (user is null) 
            throw new NotFoundException($"Not found user with id: {request.UserId}");
        
        var company = await dbContext.Companies
            .AsNoTracking()
            .Include(c => c.Employees)
            .Where(c => c.Employees.Any(e => e.UserId == request.UserId))
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (company is null)
            throw new NotFoundException($"Not found company with id: {request.Id}");
        
        return mapper.Map<CompanyDto>(company);
    }
}
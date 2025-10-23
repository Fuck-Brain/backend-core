using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileCore.Application.Commands.Company;
using ProfileCore.Application.Dtos;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Infrastructure.Database;


namespace ProfileCore.Application.Handlers.Company;

public class CreateCompanyHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.OwnerId, cancellationToken);
        if (user is null)
            throw new NotFoundException($"Not found user with id: {request.OwnerId}");

        var company = Domain.Aggregate.Company.Create(request.Name, user);
        
        dbContext.Companies.Add(company);
        dbContext.SaveChanges();
        return mapper.Map<CompanyDto>(company);
    }
}

public class DeleteCompanyHandler(ApplicationDbContext dbContext) : IRequestHandler<DeleteCompanyCommand>
{
    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (company is null)
            throw new NotFoundException($"Not found company with id: {request.Id}");
        dbContext.Companies.Remove(company);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

public class UpdateCompanyHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
    public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (company is null)
            throw new NotFoundException($"Not found company with id: {request.Id}");
        company.Rename(request.Name);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<CompanyDto>(company);
    }
}
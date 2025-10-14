using AutoMapper;
using MediatR;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Queries.Company;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Domain.IRepository;

namespace ProfileCore.Application.Handlers.Company;

public class QueryAllCompaniesHandler(ICompanyRepository companyRepository, IMapper mapper) : IRequestHandler<QueryAllCompanies, List<CompanyDto>>
{
    public async Task<List<CompanyDto>> Handle(QueryAllCompanies request, CancellationToken cancellationToken)
    {
        return mapper.Map<List<CompanyDto>>(await companyRepository.GetAllCompanyAsync());
    }
}

public class QueryCompanyByIdHandler(ICompanyRepository companyRepository, IMapper mapper) : IRequestHandler<QueryCompanyById, CompanyDto>
{
    public async Task<CompanyDto> Handle(QueryCompanyById request, CancellationToken cancellationToken)
    {
        var company = await companyRepository.GetByIdAsync(request.Id);
        if (company is null)
            throw new NotFoundException($"Not found company with id: {request.Id}");
        return mapper.Map<CompanyDto>(company);
    }
}
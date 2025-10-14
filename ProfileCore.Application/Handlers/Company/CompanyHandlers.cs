using AutoMapper;
using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands.Company;
using ProfileCore.Application.Dtos;
using ProfileCore.Domain.Aggregate;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Domain.IRepository;

namespace ProfileCore.Application.Handlers.Company;

public class CreateCompanyHandler(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IUserRepository userRepository, IMapper mapper) : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.OwnerId);
        if (user is null)
            throw new NotFoundException($"Not found user with id: {request.OwnerId}");

        var employee = new Employee(user, true, true);
        await employeeRepository.AddAsync(employee);
        var company = new Domain.Aggregate.Company(request.Name, employee);
        await companyRepository.AddAsync(company);
        return mapper.Map<CompanyDto>(company);
    }
}

public class DeleteCompanyHandler(ICompanyRepository companyRepository) : IRequestHandler<DeleteCompanyCommand>
{
    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        await companyRepository.DeleteAsync(request.Id);
    }
}

public class UpdateCompanyHandler(ICompanyRepository companyRepository, IMapper mapper) : IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
    public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await companyRepository.GetByIdAsync(request.Id);
        if (company is null)
            throw new NotFoundException($"Not found company with id: {request.Id}");
        company.Name = request.Name;
        await companyRepository.UpdateAsync(company);
        
        return mapper.Map<CompanyDto>(company);
    }
}
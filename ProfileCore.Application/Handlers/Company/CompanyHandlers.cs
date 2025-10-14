using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands.Company;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Handlers.Company;

public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    public Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, ApiResult>
{
    public Task<ApiResult> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
    public Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
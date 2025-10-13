using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Handler;

public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
    public Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
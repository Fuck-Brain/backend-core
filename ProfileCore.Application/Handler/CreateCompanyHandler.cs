using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Handler;

public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    public Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
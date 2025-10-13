using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using ProfileCore.Application.Commands;

namespace ProfileCore.Application.Handler;

public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, ApiResult>
{
    public Task<ApiResult> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
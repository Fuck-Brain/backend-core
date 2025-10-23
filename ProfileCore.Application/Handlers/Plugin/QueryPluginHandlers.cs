using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileCore.Application.Dtos;
using ProfileCore.Application.Queries.Plugin;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Infrastructure.Database;

namespace ProfileCore.Application.Handlers.Plugin;

public class QueryCompanyPluginsHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<QueryCompanyPlugins, IEnumerable<PluginDto>>
{
    public async Task<IEnumerable<PluginDto>> Handle(QueryCompanyPlugins request, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
            .AsNoTracking()
            .Include(c => c.PluginConnections).ThenInclude(pluginConnection => pluginConnection.Plugin)
            .Include(c => c.Employees)
            .Where(c => c.Employees.Any(e => e.Id == request.UserId))
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        if (company is null)
            throw new NotFoundException("Company not found");
        
        return mapper.Map<IEnumerable<PluginDto>>(company.PluginConnections.Select(x => x.Plugin));
    }
}

public class QueryAllPluginsHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<QueryAllPlugins, IEnumerable<PluginDto>>
{
    public async Task<IEnumerable<PluginDto>> Handle(QueryAllPlugins request, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<PluginDto>>(
            await dbContext.Plugins
                .AsNoTracking()
                .ToListAsync(cancellationToken)
        );
    }
}
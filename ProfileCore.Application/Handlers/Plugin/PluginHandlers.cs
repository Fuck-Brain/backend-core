using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileCore.Application.Commands.Plugin;
using ProfileCore.Application.Dtos;
using ProfileCore.Domain.Entity;
using ProfileCore.Domain.Exceptions;
using ProfileCore.Infrastructure.Database;

namespace ProfileCore.Application.Handlers.Plugin;

public class AddCompanyPluginHandler(ApplicationDbContext dbContext) : IRequestHandler<AddCompanyPluginCommand>
{
    public async Task Handle(AddCompanyPluginCommand request, CancellationToken cancellationToken)
    {
        var isAnyCompany = await dbContext.Companies.AsNoTracking().AnyAsync(c => c.Id == request.CompanyId, cancellationToken);
        if (!isAnyCompany)
            throw new NotFoundException("Company does not exist");
        var isAnyPlugins = await dbContext.Plugins.AsNoTracking().AnyAsync(p => p.Id == request.PluginId, cancellationToken);
        if (!isAnyPlugins)
            throw new NotFoundException("Plugin does not exist");
        
        var connection = PluginConnection.Create(request.CompanyId, request.PluginId);
        await dbContext.PluginConnections.AddAsync(connection, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

public class DeleteCompanyPluginHandler(ApplicationDbContext dbContext) : IRequestHandler<DeleteCompanyPluginCommand>
{
    public async Task Handle(DeleteCompanyPluginCommand request, CancellationToken cancellationToken)
    {
        var connection = await dbContext.PluginConnections
            .FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId && c.PluginId == request.PluginId, cancellationToken);
        if (connection is null)
            throw new NotFoundException("Plugin is not connected to company");
        dbContext.PluginConnections.Remove(connection);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

public class AddPluginHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<AddPluginCommand, PluginDto>
{
    public async Task<PluginDto> Handle(AddPluginCommand request, CancellationToken cancellationToken)
    {
        var plugin = Domain.Entity.Plugin.Create(request.Name, request.Description);
        await dbContext.Plugins.AddAsync(plugin, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<PluginDto>(plugin);
    }
}

public class DeletePluginHandler(ApplicationDbContext dbContext) : IRequestHandler<DeletePluginCommand>
{
    public async Task Handle(DeletePluginCommand request, CancellationToken cancellationToken)
    {
        var plugin = await dbContext.Plugins
            .FirstOrDefaultAsync(p => p.Id == request.Id);
        if (plugin is null)
            throw new NotFoundException("Plugin is not exists");
        
        dbContext.Plugins.Remove(plugin);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
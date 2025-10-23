using ProfileCore.Domain.Aggregate;
using ProfileCore.Domain.DomainAbstract;

namespace ProfileCore.Domain.Entity;

public class PluginConnection : BaseEntity, IAuditable
{
    public Guid PluginId { get; private set; }
    public Guid CompanyId { get; private set; }
    public Plugin Plugin { get; private set; }
    public Company Company { get; private set; }

    private PluginConnection() { }
    private PluginConnection(Guid id, Guid pluginId, Guid companyId)
    {
        Id = id;
        PluginId = pluginId;
        CompanyId = companyId;
    }
    
    private PluginConnection(Guid id, Plugin plugin, Company company)
    {
        Id = id;
        Plugin = plugin;
        Company = company;
    }

    public static PluginConnection Create(Company company, Plugin plugin)
    {
        if (company == null ||  plugin == null) 
            throw new ArgumentException("Cannot create connection with empty entity");
        
        return new PluginConnection(Guid.NewGuid(), plugin, company);
    }
    
    public static PluginConnection Create(Guid company, Guid plugin)
    {
        return new PluginConnection(Guid.NewGuid(), plugin, company);
    }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
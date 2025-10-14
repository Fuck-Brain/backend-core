using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using ProfileCore.Application;
using ProfileCore.Domain.IRepository;
using ProfileCore.Infrastructure.Database;
using ProfileCore.Infrastructure.Database.Repository;

namespace ProfileCore.UI.Api.Definitions.Database
{
    /// <summary>
    /// EF database content registration
    /// </summary>
    public class DatabaseDefinition : ApplicationDefinition
    {
        /// <inheritdoc />
        public override Task ConfigureServicesAsync(IDefinitionServiceContext context)
        {
            string migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name!;
            string connectionString = context.Configuration.GetConnectionString("DefaultConnection")
                                      ?? $"Server=localhost;Port=5432;User Id=postgres;Password=password;Database={AppData.ServiceName}";

            context.ServiceCollection.AddDbContext<ApplicationDbContext>(options =>
                //TODO: change your db provider 
                options.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly(migrationsAssembly)));
            context.ServiceCollection.AddScoped<IUserRepository, UserPostgreRepository>();
            context.ServiceCollection.AddScoped<IEmployeeRepository, EmployeePostgreRepository>();
            context.ServiceCollection.AddScoped<ICompanyRepository, CompanyPostgreRepository>();
            context.ServiceCollection.AddScoped<IPluginRepository, PluginPostgreRepository>();


            return base.ConfigureServicesAsync(context);
        }
    }
}

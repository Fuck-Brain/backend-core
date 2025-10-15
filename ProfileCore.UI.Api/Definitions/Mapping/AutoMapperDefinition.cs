using AutoMapper;
using Microsoft.Extensions.Logging;
using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using ProfileCore.Domain.Aggregate;
using ProfileCore.Infrastructure.Database.Mappers;

namespace ProfileCore.UI.Api.Definitions.Mapping
{
    /// <summary>
    /// Register auto mapper
    /// </summary>
    public class AutoMapperDefinition : ApplicationDefinition
    {
        /// <inheritdoc />
        public override Task ConfigureServicesAsync(IDefinitionServiceContext context)
        {
			context.ServiceCollection
				   .AddAutoMapper(typeof(Program).Assembly)
				   .AddInfrastructureMappings();
			
            return base.ConfigureServicesAsync(context);
        }

        /// <inheritdoc />
        public override Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
        {
            var webContext = context.Parse<WebDefinitionApplicationContext>();

            var mapper = webContext.ServiceProvider.GetRequiredService<AutoMapper.IConfigurationProvider>();
            if (webContext.WebApplication.Environment.IsDevelopment() || webContext.WebApplication.Environment.EnvironmentName == "Local")
            {
                mapper.AssertConfigurationIsValid();
            }
            else
            {
                mapper.CompileMappings();
            }

            return base.ConfigureApplicationAsync(context);
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
namespace ProfileCore.Infrastructure.Database.Mappers;

public static class MappingModule
{
	public static IServiceCollection AddInfrastructureMappings(this IServiceCollection services)
	{
		services.AddAutoMapper(
			typeof(CompanyMappingProfile),
			typeof(EmployeeMappingProfile),
			typeof(UserMappingProfile),
			typeof(PluginMappingProfile));
		
		return services;
	}
}
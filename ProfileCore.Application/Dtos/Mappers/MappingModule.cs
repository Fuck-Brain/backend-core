using Microsoft.Extensions.DependencyInjection;
namespace ProfileCore.Application.Dtos.Mappers;

public static class MappingModule
{
	public static IServiceCollection AddApplicationMappings(this IServiceCollection services)
	{
		services.AddAutoMapper(
			typeof(CompanyProfile),
			typeof(UserProfileProfile));
		
		return services;
	}
}
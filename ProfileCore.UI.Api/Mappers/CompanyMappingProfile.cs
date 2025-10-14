using AutoMapper;
using ProfileCore.Application.Commands.Company;
using ProfileCore.Domain.Aggregate;
using ProfileCore.UI.Api.DTOs;

namespace ProfileCore.UI.Api.Mappers
{
	public class CompanyMappingProfile : Profile
	{
		public CompanyMappingProfile()
		{
			CreateMap<Company, CompanyDto>()
				.ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
				.ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
				.ForCtorParam("OwnerId", opt => opt.MapFrom(src => src.Owner.Id))
				.ForCtorParam("PluginIds", opt => opt.MapFrom(src => src.Plugins.Select(x => x.Id).ToList()))
				.ForCtorParam("EmployeeIds", opt => opt.MapFrom(src => src.Employees.Select(x => x.Id).ToList()));

            
			CreateMap<CompanyCreateRequest, CreateCompanyCommand>()
				.ConstructUsing(src => new CreateCompanyCommand(src.Name, src.OwnerId));
            
			CreateMap<CompanyUpdateRequest, UpdateCompanyCommand>()
				.ConstructUsing((src, ctx) => 
				{
					return new UpdateCompanyCommand(src.Id, src.Name);
				});
		}
	}
}
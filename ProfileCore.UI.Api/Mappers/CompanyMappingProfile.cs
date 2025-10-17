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
			CreateMap<Company, CompanyDto>();
            
			CreateMap<CompanyCreateRequest, CreateCompanyCommand>()
				.ConstructUsing(src => new CreateCompanyCommand(src.Name, src.OwnerId));
            
			CreateMap<CompanyUpdateRequest, UpdateCompanyCommand>()
				.ConstructUsing(src => new UpdateCompanyCommand(src.Id, src.Name));
		}
	}
}


using AutoMapper;
using ProfileCore.Application.Dtos;
using ProfileCore.UI.Api.DTOs;

namespace ProfileCore.UI.Api.Mappers
{
	public class ProfileMappingProfile : Profile
	{
		public ProfileMappingProfile()
		{
			CreateMap<UserProfileDto, ProfileDto>();
		}
	}
}
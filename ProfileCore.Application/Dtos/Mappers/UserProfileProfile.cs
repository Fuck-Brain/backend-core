using AutoMapper;
using ProfileCore.Domain.Entity;

namespace ProfileCore.Application.Dtos.Mappers;

public class UserProfileProfile : Profile
{
    public UserProfileProfile()
    {
        CreateMap<UserProfile, UserProfileDto>();
    }
}
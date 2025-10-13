using AutoMapper;
using ProfileCore.Domain.Entity;
using ProfileCore.Infrastructure.Database.Entities;

namespace ProfileCore.Infrastructure.Database.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, User>().ReverseMap();
        }
    }
}
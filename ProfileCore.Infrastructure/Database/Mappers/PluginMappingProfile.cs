using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProfileCore.Domain.Entity;
using ProfileCore.Infrastructure.Database.Entities;

namespace ProfileCore.Infrastructure.Database.Mappers
{
    public class PluginMappingProfile : Profile
    {
        public PluginMappingProfile() 
        {
            CreateMap<PluginEntity, Plugin>().ReverseMap();
        }
    }
}

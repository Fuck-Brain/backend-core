using AutoMapper;
using ProfileCore.Domain.Entity;

namespace ProfileCore.Application.Dtos.Mappers;

public class PluginProfile : Profile
{
    public PluginProfile()
    {
        CreateMap<Plugin, PluginDto>();
    }
}
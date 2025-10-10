using AutoMapper;
using ProfileCore.Domain.Entity;

namespace ProfileCore.Application.Dtos.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<ProductDto, Product>()
                .ReverseMap();
        }
    }
}
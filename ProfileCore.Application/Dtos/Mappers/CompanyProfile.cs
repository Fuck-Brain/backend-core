using AutoMapper;
using ProfileCore.Domain.Aggregate;

namespace ProfileCore.Application.Dtos.Mappers;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>();
    }
}
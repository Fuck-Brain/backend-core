using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProfileCore.Domain.Aggregate;
using ProfileCore.Domain.Entity;
using ProfileCore.Infrastructure.Database.Entities;

namespace ProfileCore.Infrastructure.Database.Mappers
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile() 
        {
			CreateMap<CompanyEntity, Company>()
				.ForMember(d => d.Owner, o => o.MapFrom(s => s.Owner))
				.ForMember(d => d.Employees, o => o.MapFrom(s => s.Employees))
				.ForMember(d => d.Plugins, o => o.MapFrom(s => s.Plugins))
				.ReverseMap()
				.ForMember(d => d.Owner, o => o.MapFrom(s => s.Owner))
				.ForMember(d => d.Employees, o => o.MapFrom(s => s.Employees))
				.ForMember(d => d.Plugins, o => o.MapFrom(s => s.Plugins));
		}
    }
}

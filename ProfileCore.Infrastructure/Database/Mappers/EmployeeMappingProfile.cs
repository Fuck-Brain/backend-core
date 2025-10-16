using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using ProfileCore.Infrastructure.Database.Entities;
using ProfileCore.Domain.Aggregate;

namespace ProfileCore.Infrastructure.Database.Mappers
{
    internal class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
			CreateMap<EmployeeEntity, Employee>()
				.ForCtorParam("user", opt => opt.MapFrom(src => src.User))
				.ReverseMap()
				.ForCtorParam("user", opt => opt.MapFrom(src => src.User));
        }
    }
}

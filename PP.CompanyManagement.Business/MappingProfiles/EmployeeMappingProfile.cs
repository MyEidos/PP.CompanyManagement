using AutoMapper;
using PP.CompanyManagement.Core;
using PP.CompanyManagement.Core.Contracts.Dto;
using PP.CompanyManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Business.MappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<EmployeeEntity.BussinessIdObj, EmployeeBusinessIdDto>().ReverseMap();
            CreateMap<EmployeeEntity, EmployeeDto>();
            CreateMap<CreateUpdateEmployeeDto, EmployeeEntity>();
        }
    }
}

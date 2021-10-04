using AutoMapper;
using PP.CompanyManagement.Core;
using PP.CompanyManagement.Core.Contracts.Dto;
using PP.CompanyManagement.Portal.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Portal.MappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<EmployeeDto, GetEmployeesResponse.EmployeeViewModel>();
            CreateMap<EmployeeBusinessIdDto, EmployeeBusinessId>().ReverseMap();
            CreateMap<CreateEmployeeRequest, CreateUpdateEmployeeDto>();
            CreateMap<UpdateEmployeeRequest, CreateUpdateEmployeeDto>();
            CreateMap<EmployeeDto, UpdateEmployeeResponse>();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PP.CompanyManagement.Core.Business.Managers;
using PP.CompanyManagement.Core.Contracts.Dto;
using PP.CompanyManagement.Portal.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Portal.Controllers
{
    [ApiController]
    [Route("employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager employeeManager;
        private readonly IMapper mapper;
        public EmployeeController(IEmployeeManager employeeManager, IMapper mapper)
        {
            this.employeeManager = employeeManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<GetEmployeesResponse> List()
        {
            var employeeDtos = await this.employeeManager.GetAllAsync();

            return new GetEmployeesResponse()
            {
                List = this.mapper.Map<IEnumerable<GetEmployeesResponse.EmployeeViewModel>>(employeeDtos)
            };
        }

        [HttpPost]
        public async Task<CreateEmployeeResponse> Create(CreateEmployeeRequest employeeViewModel)
        {
            var employeeDto = this.mapper.Map<CreateUpdateEmployeeDto>(employeeViewModel);

            var createRes = await this.employeeManager.Create(employeeDto);

            return new CreateEmployeeResponse() { Id = createRes.Id };
        }

        [HttpPut("{id}")]
        public async Task<UpdateEmployeeResponse> Update(UpdateEmployeeRequest employeeViewModel, Guid id)
        {
            var employeeDto = this.mapper.Map<CreateUpdateEmployeeDto>(employeeViewModel);

            var createRes = await this.employeeManager.Update(id, employeeDto);

            return this.mapper.Map<UpdateEmployeeResponse>(createRes);
        }
    }
}

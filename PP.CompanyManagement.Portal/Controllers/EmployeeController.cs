using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.CodeGeneration.TypeScript;
using NSwag;
using NSwag.CodeGeneration.TypeScript;
using PP.CompanyManagement.Core.Business.Managers;
using PP.CompanyManagement.Core.Contracts.Dto;
using PP.CompanyManagement.Portal.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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
            //TODO: paging, sorting, etc.
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

        [HttpPost("import")]
        public async Task<IEnumerable<ImportResultResponse>> Import(CancellationToken ct)
        {
            //TODO: ? https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0#upload-large-files-with-streaming
            IFormFile file = Request.Form.Files[0];

            using var stream = file.OpenReadStream();

            // breaks the rule "use bviewmodels only" for performance reasons.
            //low batchsize for tests
            var res = await this.employeeManager.Import(stream, batchSize: 3, cancellationToken: ct);

            return res.Select(x => new ImportResultResponse()
            {
                ActionType = x.ActionType,
                BusinessIdPersonnelNumber = x.BusinessIdPersonnelNumber,
                BusinessIdType = x.BusinessIdType,
                Id = x.Id
            });
        }
    }
}

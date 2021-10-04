using AutoMapper;
using PP.CompanyManagement.Core;
using PP.CompanyManagement.Core.Business.Managers;
using PP.CompanyManagement.Core.Contracts.Dto;
using PP.CompanyManagement.Core.Entities;
using PP.CompanyManagement.Core.Exceptions.Dto;
using PP.CompanyManagement.Core.Exceptions.Employee;
using PP.CompanyManagement.Core.Interfaces.Persistence.Common;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagmentMainDbStorage.Context;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagmentMainDbStorage.Repositories;
using PP.CompanyManagement.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Business.Managers
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IUnitOfWork<ICompanyManagmentMainDbContext> unitOfWork;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public EmployeeManager(IUnitOfWork<ICompanyManagmentMainDbContext> unitOfWork,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            return this.mapper.Map<IEnumerable<EmployeeDto>>(await this.employeeRepository.SelectAllAsync());
        }

        public async Task<EmployeeDto> Create(CreateUpdateEmployeeDto employeeDto)
        {
            this.ValidateForCreateOrUpdate(employeeDto);

            // TODO: check if already exists?

            var entity = this.mapper.Map<EmployeeEntity>(employeeDto);

            this.employeeRepository.Insert(entity);

            await this.unitOfWork.SaveAsync();

            return this.mapper.Map<EmployeeDto>(entity);
        }

        public async Task<EmployeeDto> Update(Guid id, CreateUpdateEmployeeDto employeeDto)
        {
            this.ValidateForCreateOrUpdate(employeeDto);

            // TODO: check by business ID?

            var existing = await this.employeeRepository.FindAsync(id) ?? throw new EmployeeNotFoundException("Employee");

            this.mapper.Map(employeeDto, existing);

            this.employeeRepository.Update(existing);

            await this.unitOfWork.SaveAsync();

            return this.mapper.Map<EmployeeDto>(existing);
        }

        private void ValidateForCreateOrUpdate(CreateUpdateEmployeeDto employeeDto)
        {
            List<string> errors = new List<string>();
            this.ValidateBusinessId(ref errors, employeeDto.BusinessId);

            if (errors.Any())
            {
                throw new CreateEmployeeValidationException(errors, "Invalid Employee.");
            }
        }

        private void ValidateBusinessId(ref List<string> errors, EmployeeBusinessIdDto employeeBusinessIdDto)
        {
            if (employeeBusinessIdDto == null)
            {
                errors.Add("BusinessId object required.");
            }
            else if (!EmployeeBusinessIdValidatorHelper.TryValidateEmployeeBusinessId(employeeBusinessIdDto.PersonnelNumber, employeeBusinessIdDto.Type, out IEnumerable<string> bIdErrors))
            {
                errors.AddRange(bIdErrors);
            }
        }
    }
}

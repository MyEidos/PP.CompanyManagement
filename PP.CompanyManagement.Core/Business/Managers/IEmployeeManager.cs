using PP.CompanyManagement.Core.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Business.Managers
{
    public interface IEmployeeManager
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> Create(CreateUpdateEmployeeDto employeeDto);

        Task<EmployeeDto> Update(Guid id, CreateUpdateEmployeeDto employeeDto);
    }
}

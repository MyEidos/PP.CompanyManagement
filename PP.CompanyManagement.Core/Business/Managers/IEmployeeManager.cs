using PP.CompanyManagement.Core.Contracts.Dto;
using PP.CompanyManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Business.Managers
{
    public interface IEmployeeManager
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> Create(CreateUpdateEmployeeDto employeeDto);

        Task<EmployeeDto> Update(Guid id, CreateUpdateEmployeeDto employeeDto);

        Task<IEnumerable<ImportEmployeeResult>> Import(Stream jsonStream,
                    int batchSize = 1000,
                    IProgress<int> progress = null,
                    CancellationToken cancellationToken = default);
    }
}

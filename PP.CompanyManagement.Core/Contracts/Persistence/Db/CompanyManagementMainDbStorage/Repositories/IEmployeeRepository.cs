using PP.CompanyManagement.Core.Contracts.Dto;
using PP.CompanyManagement.Core.Entities;
using PP.CompanyManagement.Core.Interfaces.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagementMainDbStorage.Repositories
{
    public interface IEmployeeRepository :  IRepositoryBase<EmployeeEntity>
    {
        Task<IEnumerable<ImportEmployeeResult>> Import(IEnumerable<ImportEmployeeDto> employees, CancellationToken cancellationToken = default);
    }
}

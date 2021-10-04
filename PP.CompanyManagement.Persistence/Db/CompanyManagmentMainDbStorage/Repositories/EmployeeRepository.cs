using PP.CompanyManagement.Core.Entities;
using PP.CompanyManagement.Core.Interfaces.Persistence.Common;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagmentMainDbStorage.Context;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagmentMainDbStorage.Repositories;
using PP.CompanyManagement.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagmentMainDbStorage.Repositories
{
    public class EmployeeRepository : RepositoryBase<EmployeeEntity, ICompanyManagmentMainDbContext>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork<ICompanyManagmentMainDbContext> unitOfWork) : base(unitOfWork)
        {

        }
    }
}

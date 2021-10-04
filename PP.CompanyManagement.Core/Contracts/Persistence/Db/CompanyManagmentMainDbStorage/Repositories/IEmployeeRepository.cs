﻿using PP.CompanyManagement.Core.Entities;
using PP.CompanyManagement.Core.Interfaces.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagmentMainDbStorage.Repositories
{
    public interface IEmployeeRepository :  IRepositoryBase<EmployeeEntity>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Portal.ViewModels.Employee
{
    public record CreateEmployeeResponse
    {
        public Guid Id { get; init; }
    }
}

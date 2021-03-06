using PP.CompanyManagement.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Portal.ViewModels.Employee
{
    public record ImportResultResponse
    {
        public string ActionType { get; set; }
        public Guid Id { get; set; }
        public string BusinessIdPersonnelNumber { get; set; }
        public EmployeeType? BusinessIdType { get; set; }
    }
}

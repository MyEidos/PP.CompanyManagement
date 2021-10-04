using PP.CompanyManagement.Core.Contracts.Common;
using PP.CompanyManagement.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Contracts.Dto
{
    public record EmployeeBusinessIdDto
    {
        public string? PersonnelNumber { get; init; }

        public EmployeeType Type { get; init; }
    }
}

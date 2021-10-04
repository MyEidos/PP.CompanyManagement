using PP.CompanyManagement.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Contracts.Dto
{
    public record CreateUpdateEmployeeDto
    {
        //public Guid? Id { get; init; }

        public string? FirstName { get; init; }

        public string? MiddleName { get; init; }

        public string? LastName { get; init; }

        public Gender? Gender { get; init; }

        public DateTime? DOB { get; init; }

        public EmployeeBusinessIdDto BusinessId { get; init; }
    }
}

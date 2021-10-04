using PP.CompanyManagement.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Portal.ViewModels.Employee
{
    public record UpdateEmployeeRequest
    {
        public string? FirstName { get; init; }

        public string? MiddleName { get; init; }

        public string? LastName { get; init; }

        [Required]
        public EmployeeBusinessId BusinessId { get; init; }

        public Gender? Gender { get; init; }

        public DateTime? DOB { get; init; }
    }
}

using PP.CompanyManagement.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Portal.ViewModels.Employee
{
    public record GetEmployeesResponse
    {
        public IEnumerable<EmployeeViewModel> List { get; init; } = new List<EmployeeViewModel>();

        public record EmployeeViewModel
        {
            public Guid Id { get; init; }

            public string? FirstName { get; init; }

            public string? MiddleName { get; init; }

            public string? LastName { get; init; }

            public EmployeeBusinessId BusinessId { get; init; }

            public Gender? Gender { get; init; }

            public DateTime? DOB { get; init; }
        }
    }
}

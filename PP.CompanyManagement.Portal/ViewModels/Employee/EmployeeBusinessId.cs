using PP.CompanyManagement.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Portal.ViewModels.Employee
{
    public record EmployeeBusinessId : IValidatableObject
    {
        public string? PersonnelNumber { get; init; }

        public EmployeeType Type { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Type ==  EmployeeType.Staff && string.IsNullOrWhiteSpace(PersonnelNumber))
            {
                yield return new ValidationResult(
                    $"PersonnelNumber should have value for Staff.",
                    new[] { nameof(PersonnelNumber) });
            }

            if (Type == EmployeeType.Supplementary && !string.IsNullOrWhiteSpace(PersonnelNumber))
            {
                yield return new ValidationResult(
                    $"PersonnelNumber should be empty for Supplementary.",
                    new[] { nameof(PersonnelNumber) });
            }
        }
    }
}

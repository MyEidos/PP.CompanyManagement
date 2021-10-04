using PP.CompanyManagement.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Validators
{
    public static class EmployeeBusinessIdValidatorHelper
    {
        public static bool TryValidateEmployeeBusinessId(string? personnelNumber, EmployeeType? type, out IEnumerable<string> errors)
        {
            errors = new List<string>();
            List<string> errorsTyped = (List<string>) errors;

            if (!type.HasValue)
            {
                errorsTyped.Add("Type is required.");
                return false;
            }

            //TODO:use switch?
            if(type == EmployeeType.Staff && personnelNumber == null)
            {
                errorsTyped.Add("PersonnelNumber is required when Type is Staff.");
                return false;
            }

            if (type == EmployeeType.Supplementary && personnelNumber != null)
            {
                errorsTyped.Add("PersonnelNumber should be NULL when Type is Supplementary.");
                return false;
            }

            return true;
        }
    }
}

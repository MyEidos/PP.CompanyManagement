using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Exceptions.Dto
{
    public class CreateEmployeeValidationException : ValidationException
    {
        public CreateEmployeeValidationException(IEnumerable<string> errors, string? message = null)
            : base(errors, message)
        {
        }
    }
}

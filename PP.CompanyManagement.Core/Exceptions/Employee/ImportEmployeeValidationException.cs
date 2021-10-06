using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Exceptions.Dto
{
    public class ImportEmployeeValidationException : ValidationException
    {
        public ImportEmployeeValidationException(IEnumerable<string> errors, string? message = null, Exception? innerException = null)
            : base(errors, message, innerException)
        {
        }
    }
}

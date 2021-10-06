using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Exceptions
{
    public abstract class ValidationException : CustomBaseException
    {
        protected ValidationException( IEnumerable<string> errors, string? message = null, Exception? innerException = null)
            : base(ToMessage(errors, message), innerException)
        {
        }

        private static string ToMessage(IEnumerable<string> errors, string? message = null)
        {
            string msg = string.Empty;
            if(message != null)
            {
                msg = message + Environment.NewLine;
            }

            return msg
                + errors != null && errors.Any() ? string.Join(Environment.NewLine, errors) : string.Empty;
        }
    }
}

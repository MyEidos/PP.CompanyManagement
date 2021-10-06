using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string name, string? message = null, Exception? innerException = null)
            : base(ToMessage(name, message), innerException)
        {
        }

        private static string ToMessage(string name, string? message = null)
        {
            string msg = $"{name} not found.";
            if (message != null)
            {
                msg = message + Environment.NewLine;
            }

            return msg;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Exceptions
{
    public abstract class CustomBaseException : Exception
    {
        protected CustomBaseException(string message)
            : base(message)
        {
        }
    }
}

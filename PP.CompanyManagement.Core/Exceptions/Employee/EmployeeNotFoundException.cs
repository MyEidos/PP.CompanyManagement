using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Exceptions.Employee
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(string name, string? message = null) : base(name, message)
        {

        }
    }
}

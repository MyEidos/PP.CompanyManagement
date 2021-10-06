using PP.CompanyManagement.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Entities
{
    public class ImportEmployeeResult
    {
        public string ActionType {get; set;}
        public Guid Id { get; set; }
        public string BusinessIdPersonnelNumber { get; set; }
        public EmployeeType? BusinessIdType { get; set; }
    }
}

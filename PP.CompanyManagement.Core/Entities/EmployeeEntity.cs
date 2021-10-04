using PP.CompanyManagement.Core.Contracts.Common;
using PP.CompanyManagement.Core.Entities.Shared;
using PP.CompanyManagement.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Entities
{
    public class EmployeeEntity : BaseSimpleGuidIdentifierEntity
    {
        
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? DOB { get; set; }

        public BussinessIdObj BusinessId { get; set; }

        public class BussinessIdObj : ValueObject
        {
            public string? PersonnelNumber { get; set; }

            public EmployeeType Type { get; set; }

            protected override IEnumerable<object?> GetEqualityComponents()
            {
                yield return this.PersonnelNumber;
                yield return this.Type;
            }
        }
    }
}

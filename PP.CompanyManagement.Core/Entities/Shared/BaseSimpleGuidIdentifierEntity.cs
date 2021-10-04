using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Entities.Shared
{
    public class BaseSimpleGuidIdentifierEntity : EntityBase
    {
        public Guid Id { get; set; }
    }
}

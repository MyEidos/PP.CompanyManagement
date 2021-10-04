using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Core.Entities.Shared
{
    public abstract class EntityBase
    {
        public bool IsActive { get; set; } = true;

        public DateTimeOffset? Created { get; set; }

        public DateTimeOffset? Updated { get; set; }
    }
}

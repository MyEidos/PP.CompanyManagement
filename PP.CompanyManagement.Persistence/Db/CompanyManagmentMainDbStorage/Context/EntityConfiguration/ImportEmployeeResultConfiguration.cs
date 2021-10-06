using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PP.CompanyManagement.Core.Entities;
using PP.CompanyManagement.Core.Shared.Enums;
using PP.CompanyManagement.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagementMainDbStorage.Context.EntityConfiguration
{
    public class ImportEmployeeResultConfiguration : IEntityTypeConfiguration<ImportEmployeeResult>
    {
        public void Configure(EntityTypeBuilder<ImportEmployeeResult> builder)
        {
            builder.HasNoKey().ToView(null);
            builder.Property(x => x.BusinessIdPersonnelNumber).HasColumnName("BusinessId_PersonnelNumber");
            builder.Property(x => x.BusinessIdType).HasColumnName("BusinessId_Type");
        }
    }
}

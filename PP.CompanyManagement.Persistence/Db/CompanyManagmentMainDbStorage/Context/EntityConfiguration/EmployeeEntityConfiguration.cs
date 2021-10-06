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
    public class EmployeeEntityConfiguration : EntityConfigurationBase<EmployeeEntity>
    {
        public override void ConfigureMap(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(300);
            builder.Property(x => x.LastName).HasMaxLength(300);
            builder.Property(x => x.MiddleName).HasMaxLength(300);
            builder.Property(x => x.DOB).HasColumnType("date");

            builder.OwnsOne(x => x.BusinessId, oe =>
            {
                oe.Property(x => x.PersonnelNumber).HasMaxLength(500);
                oe.Property(x => x.Type).IsRequired();
                oe.HasIndex(x => new { x.Type, x.PersonnelNumber }).IsUnique();
            });

            var tableName = builder.Metadata.GetTableName();
            var numberColName = builder.OwnsOne(x => x.BusinessId)
                .Property(x => x.PersonnelNumber).Metadata.GetColumnName(StoreObjectIdentifier.Table(tableName, builder.Metadata.GetSchema()));
            var typeColName = builder.OwnsOne(x => x.BusinessId)
                .Property(x => x.Type).Metadata.GetColumnName(StoreObjectIdentifier.Table(tableName, builder.Metadata.GetSchema()));

            builder.HasCheckConstraint($"CK_{tableName}_{numberColName}_{typeColName}", $"([{typeColName}] = {(int)EmployeeType.Staff} AND [{numberColName}] IS NOT NULL) OR ([{typeColName}] = {(int)EmployeeType.Supplementary} AND [{numberColName}] IS NULL)");
        }
    }
}

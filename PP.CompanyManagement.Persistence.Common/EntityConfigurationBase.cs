using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PP.CompanyManagement.Core.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Common
{
    public abstract class EntityConfigurationBase<T> : IEntityTypeConfiguration<T>
        where T : EntityBase
    {
        public abstract void ConfigureMap(EntityTypeBuilder<T> builder);

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.IsActive)
                .HasDefaultValue(true)
                .HasComment("Value indicating whether the entity is active.");
            builder.Property(x => x.Created)
                .HasComment("Entity created date time.");
            builder.Property(x => x.Updated)
                .HasComment("Entity last updated date time.");

            builder.HasQueryFilter(x => x.IsActive);

            this.ConfigureMap(builder);
        }
    }
}

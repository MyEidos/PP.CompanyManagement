using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PP.CompanyManagement.Core.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Common
{
    public class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions options)
            : base(options)
        {
        }

#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }));

            base.OnConfiguring(optionsBuilder);
        }
#endif

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.OnBeforeSaveChanges();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            this.OnBeforeSaveChanges();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.OnBeforeSaveChanges();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.OnBeforeSaveChanges();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaveChanges()
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is EntityBase && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((EntityBase)entityEntry.Entity).Created = DateTimeOffset.UtcNow;
                }
                else
                {
                    ((EntityBase)entityEntry.Entity).Updated = DateTimeOffset.UtcNow;
                }
            }
        }
    }
}

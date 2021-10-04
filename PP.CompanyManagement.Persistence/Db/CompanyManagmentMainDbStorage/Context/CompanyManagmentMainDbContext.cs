using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using PP.CompanyManagement.Core.Contracts.Persistence.Db.CompanyManagmentMainDbStorage;
using PP.CompanyManagement.Core.Entities;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagmentMainDbStorage.Context;
using PP.CompanyManagement.Persistence.Common;
using PP.CompanyManagement.Persistence.Db.CompanyManagmentMainDbStorage.Context.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagmentMainDbStorage.Context
{
    public partial class CompanyManagmentMainDbContext : DbContextBase, ICompanyManagmentMainDbContext
    {
        private readonly CompanyManagmentMainStorageOptions options;
        public CompanyManagmentMainDbContext(DbContextOptions<CompanyManagmentMainDbContext> options, IOptions<CompanyManagmentMainStorageOptions> configs)
            : base(options)
        {
            this.options = configs.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(options.DbConnectionString, sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
            });
        }

        public DbSet<EmployeeEntity> Employees { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("cm");

            modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());

            // set DeleteBehavior from Cascade to Restrict.
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            this.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class CompanyManagmentMainDbContextFactory : IDesignTimeDbContextFactory<CompanyManagmentMainDbContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context for EF tools (command line, etc).
        /// Usage example (creates migration with name "init" from Package Manager Console):
        /// $env:companyManagmentMainDbsqlConnectionString= "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=.\\PP.CompanyManagement.Persistence\Db\CompanyManagmentMainDbStorage\Local\CompanyManagmentMainDB_local.mdf;Integrated Security=True"
        /// Add-Migration init -o "Db\CompanyManagmentMainDbStorage\Migrations" -Project "PP.CompanyManagement.Persistence" -StartupProject "PP.CompanyManagement.Persistence" -Verbose -Context "CompanyManagmentMainDbContext"
        /// 
        /// Apply migrations:
        /// Update-Database -Project "PP.CompanyManagement.Persistence" -StartupProject "PP.CompanyManagement.Persistence" -Verbose -Context "CompanyManagmentMainDbContext"
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of <typeparamref name="TContext" />.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Connection string is null</exception>
        public CompanyManagmentMainDbContext CreateDbContext(string[] args)
        {
            string sqlConnection = Environment.GetEnvironmentVariable("CompanyManagmentMainDbConnectionString") 
                ?? Environment.GetEnvironmentVariable("companyManagmentMainDbsqlConnectionString") 
                ?? throw new InvalidOperationException("Connection string is null. Provide 'CompanyManagmentMainDbConnectionString' enviroement variable.");

            var optionsBuilder = new DbContextOptionsBuilder<CompanyManagmentMainDbContext>();
            optionsBuilder.UseSqlServer(sqlConnection);

            return new CompanyManagmentMainDbContext(optionsBuilder.Options, Options.Create<CompanyManagmentMainStorageOptions>(new CompanyManagmentMainStorageOptions() { DbConnectionString = sqlConnection }));
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using PP.CompanyManagement.Core.Contracts.Persistence.Db.CompanyManagementMainDbStorage;
using PP.CompanyManagement.Core.Entities;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagementMainDbStorage.Context;
using PP.CompanyManagement.Persistence.Common;
using PP.CompanyManagement.Persistence.Db.CompanyManagementMainDbStorage.Context.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagementMainDbStorage.Context
{
    public partial class CompanyManagementMainDbContext : DbContextBase, ICompanyManagementMainDbContext
    {
        private readonly CompanyManagementMainStorageOptions options;
        public CompanyManagementMainDbContext(DbContextOptions<CompanyManagementMainDbContext> options, IOptions<CompanyManagementMainStorageOptions> configs)
            : base(options)
        {
            this.options = configs.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(options.DbConnectionString);
        }

        public DbSet<EmployeeEntity> Employees { get; set; } 

        //SP results
        public DbSet<ImportEmployeeResult> ImportEmployeeResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("cm");

            modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ImportEmployeeResultConfiguration());

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

    public class CompanyManagementMainDbContextFactory : IDesignTimeDbContextFactory<CompanyManagementMainDbContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context for EF tools (command line, etc).
        /// Usage example (creates migration with name "init" from Package Manager Console):
        /// $env:CompanyManagementMainDbConnectionString= "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Misc\TestProject2\PP.CompanyManagement\PP.CompanyManagement.Persistence\Db\CompanyManagmentMainDbStorage\Local\CompanyManagmentMainDb_local.mdf;Integrated Security=True"
        /// Add-Migration init -o "Db\CompanyManagementMainDbStorage\Migrations" -Project "PP.CompanyManagement.Persistence" -StartupProject "PP.CompanyManagement.Persistence" -Verbose -Context "CompanyManagementMainDbContext"
        /// 
        /// Apply migrations:
        /// Update-Database -Project "PP.CompanyManagement.Persistence" -StartupProject "PP.CompanyManagement.Persistence" -Verbose -Context "CompanyManagementMainDbContext"
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of <typeparamref name="TContext" />.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Connection string is null</exception>
        public CompanyManagementMainDbContext CreateDbContext(string[] args)
        {
            string sqlConnection = Environment.GetEnvironmentVariable("CompanyManagementMainDbConnectionString") 
                ?? Environment.GetEnvironmentVariable("companyManagementMainDbsqlConnectionString") 
                ?? throw new InvalidOperationException("Connection string is null. Provide 'CompanyManagementMainDbConnectionString' enviroement variable.");

            var optionsBuilder = new DbContextOptionsBuilder<CompanyManagementMainDbContext>();
            optionsBuilder.UseSqlServer(sqlConnection);

            return new CompanyManagementMainDbContext(optionsBuilder.Options, Options.Create<CompanyManagementMainStorageOptions>(new CompanyManagementMainStorageOptions() { DbConnectionString = sqlConnection }));
        }
    }
}

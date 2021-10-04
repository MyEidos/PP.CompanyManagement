using Microsoft.EntityFrameworkCore;
using PP.CompanyManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagmentMainDbStorage.Context
{
    public partial class CompanyManagmentMainDbContext
    {
        protected void Seed(ModelBuilder modelBuilder)
        {
            this.SeedEmployees(modelBuilder);
        }

        private void SeedEmployees(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < 10; i++)
            {
                var id = new Guid($"00000000-0000-0000-0000-0000000{i + 1:0000#}");

                modelBuilder.Entity<EmployeeEntity>().HasData(new EmployeeEntity()
                {
                    Id = id,
                    DOB = DateTime.UtcNow,
                    FirstName = "first" + i,
                    LastName = "last" + i,
                    MiddleName = "middle" + i,
                    Gender = Core.Shared.Enums.Gender.Male,
                    IsActive = true,
                    Created = DateTime.UtcNow
                });

                modelBuilder.Entity<EmployeeEntity>().OwnsOne(e => e.BusinessId).HasData(
                new
                {
                    EmployeeEntityId = id,
                    PersonnelNumber = i.ToString(),
                    Type = Core.Shared.Enums.EmployeeType.Staff
                });
            }
        }
    }
}

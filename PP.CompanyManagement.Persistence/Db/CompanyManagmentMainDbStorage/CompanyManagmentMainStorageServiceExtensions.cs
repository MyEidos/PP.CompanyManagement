using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PP.CompanyManagement.Core.Contracts.Persistence.Db.CompanyManagmentMainDbStorage;
using PP.CompanyManagement.Core.Interfaces.Persistence.Common;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagmentMainDbStorage.Context;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagmentMainDbStorage.Repositories;
using PP.CompanyManagement.Persistence.Common;
using PP.CompanyManagement.Persistence.Db.CompanyManagmentMainDbStorage.Context;
using PP.CompanyManagement.Persistence.Db.CompanyManagmentMainDbStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagmentMainDbStorage
{
    public static class CompanyManagmentMainStorageServiceExtensions
    {
        public static IServiceCollection AddCompanyManagmentMainStorage(this IServiceCollection services, Action<CompanyManagmentMainStorageOptions> configureOptions)
        {
            services.Configure<CompanyManagmentMainStorageOptions>(configureOptions);

            services.AddDbContext<ICompanyManagmentMainDbContext, CompanyManagmentMainDbContext>();

            services
                .AddScoped<IUnitOfWork<ICompanyManagmentMainDbContext>, UnitOfWork<ICompanyManagmentMainDbContext>>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}

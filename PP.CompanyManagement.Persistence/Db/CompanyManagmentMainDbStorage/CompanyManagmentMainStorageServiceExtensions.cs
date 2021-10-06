using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PP.CompanyManagement.Core.Contracts.Persistence.Db.CompanyManagementMainDbStorage;
using PP.CompanyManagement.Core.Interfaces.Persistence.Common;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagementMainDbStorage.Context;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagementMainDbStorage.Repositories;
using PP.CompanyManagement.Persistence.Common;
using PP.CompanyManagement.Persistence.Db.CompanyManagementMainDbStorage.Context;
using PP.CompanyManagement.Persistence.Db.CompanyManagementMainDbStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagementMainDbStorage
{
    public static class CompanyManagementMainStorageServiceExtensions
    {
        public static IServiceCollection AddCompanyManagementMainStorage(this IServiceCollection services, Action<CompanyManagementMainStorageOptions> configureOptions)
        {
            services.Configure<CompanyManagementMainStorageOptions>(configureOptions);

            services.AddDbContext<ICompanyManagementMainDbContext, CompanyManagementMainDbContext>();

            services
                .AddScoped<IUnitOfWork<ICompanyManagementMainDbContext>, UnitOfWork<ICompanyManagementMainDbContext>>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}

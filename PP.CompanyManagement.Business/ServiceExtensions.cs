using Microsoft.Extensions.DependencyInjection;
using PP.CompanyManagement.Business.Managers;
using PP.CompanyManagement.Core.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Business
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceExtensions));
            services.AddTransient<IEmployeeManager, EmployeeManager>();

            return services;
        }
    }
}

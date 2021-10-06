using Microsoft.EntityFrameworkCore;
using Paillave.EntityFrameworkCoreExtension.BulkSave;
using Paillave.Etl.Core;
using PP.CompanyManagement.Core.Contracts.Dto;
using PP.CompanyManagement.Core.Entities;
using PP.CompanyManagement.Core.Interfaces.Persistence.Common;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagementMainDbStorage.Context;
using PP.CompanyManagement.Core.Interfaces.Persistence.Db.CompanyManagementMainDbStorage.Repositories;
using PP.CompanyManagement.Core.Shared.Enums;
using PP.CompanyManagement.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagementMainDbStorage.Repositories
{
    public class EmployeeRepository : RepositoryBase<EmployeeEntity, ICompanyManagementMainDbContext>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork<ICompanyManagementMainDbContext> unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<ImportEmployeeResult>> Import(IEnumerable<ImportEmployeeDto> employees, CancellationToken cancellationToken = default)
        {
            DataTable table = new DataTable();
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("MiddleName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("Gender", typeof(int));
            table.Columns.Add("DOB", typeof(DateTime));
            table.Columns.Add("BusinessId_PersonnelNumber", typeof(string));
            table.Columns.Add("BusinessId_Type", typeof(int));

            foreach (var item in employees)
            {
                DataRow dr = table.NewRow();

                //TODO: review mapping types.
                dr["FirstName"] = item.FirstName;
                dr["MiddleName"] = item.MiddleName;
                dr["LastName"] = item.LastName;
                dr["Gender"] = (int) item.Gender;
                dr["DOB"] = item.DOB;
                dr["BusinessId_PersonnelNumber"] = item.BusinessId?.PersonnelNumber;
                dr["BusinessId_Type"] = item.BusinessId == null ? 0 :  (int) item.BusinessId.Type ;

                table.Rows.Add(dr);
            }

            var parameter = new SqlParameter("@employees", SqlDbType.Structured);
            parameter.Value = table;
            parameter.TypeName = "[cm].EmployeesImportType";
            return await this.DataContext.Set<ImportEmployeeResult>()
                     .FromSqlRaw("exec [cm].[Employee_Import] @employees", parameter)
                     .ToListAsync(cancellationToken);
        }

    }
}

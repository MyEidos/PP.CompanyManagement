///<reference path="IEmployeeViewModel.ts"/>

module PP.CompanyManagement.Portal.ViewModels.Employee {
	export interface IGetEmployeesResponse
	{
		List: PP.CompanyManagement.Portal.ViewModels.Employee.IEmployeeViewModel[];
	}
}

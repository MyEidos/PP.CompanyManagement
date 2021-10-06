///<reference path="../../../Core/Shared/Enums/EmployeeType.ts"/>

module PP.CompanyManagement.Portal.ViewModels.Employee {
	export interface IEmployeeBusinessId
	{
		PersonnelNumber: string;
		Type: PP.CompanyManagement.Core.Shared.Enums.EmployeeType;
	}
}

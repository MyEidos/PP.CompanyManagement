///<reference path="IEmployeeBusinessId.ts"/>
///<reference path="../../../Core/Shared/Enums/Gender.ts"/>

module PP.CompanyManagement.Portal.ViewModels.Employee {
	export interface IUpdateEmployeeRequest
	{
		FirstName: string;
		MiddleName: string;
		LastName: string;
		BusinessId: PP.CompanyManagement.Portal.ViewModels.Employee.IEmployeeBusinessId;
		Gender: PP.CompanyManagement.Core.Shared.Enums.Gender;
		DOB: any;
	}
}

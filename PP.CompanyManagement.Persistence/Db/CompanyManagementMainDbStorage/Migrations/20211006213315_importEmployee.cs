using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagementMainDbStorage.Migrations
{
    public partial class importEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" CREATE  TYPE cm.EmployeesImportType
AS TABLE
(
    [FirstName]                  NVARCHAR (300)     NULL,
    [MiddleName]                 NVARCHAR (300)     NULL,
    [LastName]                   NVARCHAR (300)     NULL,
    [Gender]                     INT                NULL,
    [DOB]                        DATE               NULL,
    [BusinessId_PersonnelNumber] NVARCHAR (500)     NULL,
    [BusinessId_Type]            INT                NOT NULL
);");

            var createProcSql = @"CREATE OR ALTER PROCEDURE [cm].[Employee_Import]
	@employees [cm].EmployeesImportType READONLY
AS
BEGIN

--DECLARE @CustomerUpdates [cm].EmployeesImportType;

--INSERT INTO @CustomerUpdates([FirstName], [BusinessId_PersonnelNumber], [BusinessId_Type])
--VALUES
--	('Firstname','idnew01',1),
--	('Firstname','idnew02',1),
--	('Firstname','idnew03',1)
--;

--exec [cm].[Employee_Import] @employees = @CustomerUpdates

-- if we need patial updates even on error, can try to implement something liek this https://www.sqlservercentral.com/articles/merge-error-handling
set nocount on;

declare @result table (ActionType varchar(300), Id uniqueidentifier, [BusinessId_PersonnelNumber] varchar(300), [BusinessId_Type] int);

merge	[cm].[Employees] as t
using	@employees as s 
on		1=1 and t.[BusinessId_PersonnelNumber] = s.[BusinessId_PersonnelNumber] and t.[BusinessId_Type] = s.[BusinessId_Type]
when matched then
	update set
	[FirstName] = s.[FirstName],
	[MiddleName] = s.[MiddleName],
	[LastName] = s.[LastName],
	[Gender] = s.[Gender],
	[DOB] = s.[DOB],
	[BusinessId_PersonnelNumber] = s.[BusinessId_PersonnelNumber],
	[BusinessId_Type] = s.[BusinessId_Type],
	[IsActive] = 1,
	[Updated] = GETDATE()
when not matched by target then
	insert([Id],[FirstName],[MiddleName],[LastName],[Gender],[DOB],[BusinessId_PersonnelNumber],[BusinessId_Type],[IsActive],[Created],[Updated])
	values(NEWID(),s.[FirstName],s.[MiddleName],s.[LastName],s.[Gender],s.[DOB],s.[BusinessId_PersonnelNumber],s.[BusinessId_Type],1,GETDATE(),GETDATE())
OUTPUT
   $action as ActionType,
   inserted.Id,
   inserted.[BusinessId_PersonnelNumber],
   inserted.[BusinessId_Type]
into
  @result (ActionType, Id, [BusinessId_PersonnelNumber], [BusinessId_Type]);

select * from @result
END";
            migrationBuilder.Sql(createProcSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //TODO: drop 
        }

    }
}

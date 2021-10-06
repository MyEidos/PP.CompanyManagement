using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagementMainDbStorage.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cm");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "cm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    DOB = table.Column<DateTime>(type: "date", nullable: true),
                    BusinessId_PersonnelNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BusinessId_Type = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Value indicating whether the entity is active."),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, comment: "Entity created date time."),
                    Updated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, comment: "Entity last updated date time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.CheckConstraint("CK_Employees_BusinessId_PersonnelNumber_BusinessId_Type", "([BusinessId_Type] = 1 AND [BusinessId_PersonnelNumber] IS NOT NULL) OR ([BusinessId_Type] = 2 AND [BusinessId_PersonnelNumber] IS NULL)");
                });

            migrationBuilder.InsertData(
                schema: "cm",
                table: "Employees",
                columns: new[] { "Id", "Created", "DOB", "FirstName", "Gender", "IsActive", "LastName", "MiddleName", "Updated", "BusinessId_PersonnelNumber", "BusinessId_Type" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2021, 10, 6, 21, 32, 42, 749, DateTimeKind.Unspecified).AddTicks(8101), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 6, 21, 32, 42, 749, DateTimeKind.Utc).AddTicks(6156), "first0", 1, true, "last0", "middle0", null, "0", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Unspecified).AddTicks(8489), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Utc).AddTicks(8479), "first1", 1, true, "last1", "middle1", null, "1", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Unspecified).AddTicks(8823), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Utc).AddTicks(8819), "first2", 1, true, "last2", "middle2", null, "2", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Unspecified).AddTicks(8984), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Utc).AddTicks(8981), "first3", 1, true, "last3", "middle3", null, "3", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTimeOffset(new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Unspecified).AddTicks(9128), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Utc).AddTicks(9125), "first4", 1, true, "last4", "middle4", null, "4", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new DateTimeOffset(new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Unspecified).AddTicks(9309), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Utc).AddTicks(9306), "first5", 1, true, "last5", "middle5", null, "5", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new DateTimeOffset(new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Unspecified).AddTicks(9444), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Utc).AddTicks(9441), "first6", 1, true, "last6", "middle6", null, "6", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new DateTimeOffset(new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Unspecified).AddTicks(9578), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Utc).AddTicks(9575), "first7", 1, true, "last7", "middle7", null, "7", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new DateTimeOffset(new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Unspecified).AddTicks(9747), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Utc).AddTicks(9743), "first8", 1, true, "last8", "middle8", null, "8", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Unspecified).AddTicks(9891), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 6, 21, 32, 42, 750, DateTimeKind.Utc).AddTicks(9888), "first9", 1, true, "last9", "middle9", null, "9", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BusinessId_Type_BusinessId_PersonnelNumber",
                schema: "cm",
                table: "Employees",
                columns: new[] { "BusinessId_Type", "BusinessId_PersonnelNumber" },
                unique: true,
                filter: "[BusinessId_Type] IS NOT NULL AND [BusinessId_PersonnelNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees",
                schema: "cm");
        }
    }
}

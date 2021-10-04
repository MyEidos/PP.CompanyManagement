using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PP.CompanyManagement.Persistence.Db.CompanyManagmentMainDbStorage.Migrations
{
    public partial class init4 : Migration
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
                    BusinessId_Type = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Value indicating whether the entity is active."),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, comment: "Entity created date time."),
                    Updated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, comment: "Entity last updated date time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.CheckConstraint("CK_Employees_BusinessId_PersonnelNumber_BusinessId_Type", "[BusinessId_PersonnelNumber] IS NOT NULL OR [BusinessId_Type] = 2");
                });

            migrationBuilder.InsertData(
                schema: "cm",
                table: "Employees",
                columns: new[] { "Id", "Created", "DOB", "FirstName", "Gender", "IsActive", "LastName", "MiddleName", "Updated", "BusinessId_PersonnelNumber", "BusinessId_Type" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2021, 10, 3, 22, 26, 35, 723, DateTimeKind.Unspecified).AddTicks(6525), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 3, 22, 26, 35, 723, DateTimeKind.Utc).AddTicks(3375), "first0", 1, true, "last0", "middle0", null, "0", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Unspecified).AddTicks(120), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Utc).AddTicks(99), "first1", 1, true, "last1", "middle1", null, "1", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Unspecified).AddTicks(599), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Utc).AddTicks(591), "first2", 1, true, "last2", "middle2", null, "2", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Unspecified).AddTicks(855), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Utc).AddTicks(806), "first3", 1, true, "last3", "middle3", null, "3", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTimeOffset(new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Unspecified).AddTicks(1057), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Utc).AddTicks(1053), "first4", 1, true, "last4", "middle4", null, "4", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new DateTimeOffset(new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Unspecified).AddTicks(1246), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Utc).AddTicks(1242), "first5", 1, true, "last5", "middle5", null, "5", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new DateTimeOffset(new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Unspecified).AddTicks(1427), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Utc).AddTicks(1422), "first6", 1, true, "last6", "middle6", null, "6", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new DateTimeOffset(new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Unspecified).AddTicks(1645), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Utc).AddTicks(1641), "first7", 1, true, "last7", "middle7", null, "7", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new DateTimeOffset(new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Unspecified).AddTicks(1826), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Utc).AddTicks(1821), "first8", 1, true, "last8", "middle8", null, "8", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Unspecified).AddTicks(2006), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 10, 3, 22, 26, 35, 725, DateTimeKind.Utc).AddTicks(2001), "first9", 1, true, "last9", "middle9", null, "9", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BusinessId_Type_BusinessId_PersonnelNumber",
                schema: "cm",
                table: "Employees",
                columns: new[] { "BusinessId_Type", "BusinessId_PersonnelNumber" },
                unique: true,
                filter: "[BusinessId_PersonnelNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees",
                schema: "cm");
        }
    }
}

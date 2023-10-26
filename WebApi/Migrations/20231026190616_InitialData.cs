using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "CommandId", "City", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("1ac59c05-7f72-45f5-bb5a-d2006998d3e7"), "Saransk", "Russia", "Mordovia" },
                    { new Guid("d960d5e1-a23e-4052-8d41-2cd31c6a9bda"), "Moscow", "Russia", "Amkal" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "312 Forest Avenue, BF 923", "USA", "Admin_Solutions Ltd" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "583 Wall Dr. Gwynn Oak, MD 21207", "USA", "IT_Solutions Ltd" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), 35, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Kane Miller", "Administrator" },
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 26, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Sam Raiden", "Software developer" },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 30, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Jana McLeaf", "Software developer" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "Age", "CommandId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("92674f7a-31f0-4a51-9331-74e82ab980c1"), 25, new Guid("1ac59c05-7f72-45f5-bb5a-d2006998d3e7"), "Andrey Zubanov", "Goalkeeper" },
                    { new Guid("a68fcbca-7f58-45e6-a1f9-aea8d263764e"), 20, new Guid("1ac59c05-7f72-45f5-bb5a-d2006998d3e7"), "Nikita Shirmankin", "Center forward" },
                    { new Guid("c53068cb-27ab-4f3f-832e-b93f5f04e263"), 19, new Guid("d960d5e1-a23e-4052-8d41-2cd31c6a9bda"), "Denis Ivanov", "Halfback" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: new Guid("92674f7a-31f0-4a51-9331-74e82ab980c1"));

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: new Guid("a68fcbca-7f58-45e6-a1f9-aea8d263764e"));

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: new Guid("c53068cb-27ab-4f3f-832e-b93f5f04e263"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "CommandId",
                keyValue: new Guid("1ac59c05-7f72-45f5-bb5a-d2006998d3e7"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "CommandId",
                keyValue: new Guid("d960d5e1-a23e-4052-8d41-2cd31c6a9bda"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}

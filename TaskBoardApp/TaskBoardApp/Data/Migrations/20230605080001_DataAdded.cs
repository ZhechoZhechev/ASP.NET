using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class DataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LasttName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c1fda018-20c1-47ea-896f-fcbb6ab608de", 0, "a883413f-3af1-4360-b15f-52af8f82509a", "guest@mail.com", false, "Guest", "User", false, null, "GUEST@MAIL.COM", "GUEST", "AQAAAAEAACcQAAAAEKc5NfGPYtZudQ3c28cq4Ljyc/PzApXHOsHi17m5ue6kMFAV/VmId02aSYHI0GbR2w==", null, false, "21fb7114-3828-46ed-b58f-7f5de88ab039", false, "Guest" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 5, 5, 11, 0, 1, 388, DateTimeKind.Local).AddTicks(9847), "Learn to use ASP.NET Core Identity", "c1fda018-20c1-47ea-896f-fcbb6ab608de", "Prepare for ASP.NET Fundamentals exam" },
                    { 2, 3, new DateTime(2023, 1, 5, 11, 0, 1, 388, DateTimeKind.Local).AddTicks(9889), "Learn using EF Core and MS SQL Server Management Studio", "c1fda018-20c1-47ea-896f-fcbb6ab608de", "Improve EF Core skills" },
                    { 3, 2, new DateTime(2023, 5, 26, 11, 0, 1, 388, DateTimeKind.Local).AddTicks(9893), "Learn using ASP.NET Core Identity", "c1fda018-20c1-47ea-896f-fcbb6ab608de", "Improve ASP.NET Core skills" },
                    { 4, 3, new DateTime(2022, 6, 5, 11, 0, 1, 388, DateTimeKind.Local).AddTicks(9895), "Prepare by solving old Mid and Final exams", "c1fda018-20c1-47ea-896f-fcbb6ab608de", "Prepare for C# Fundamentals Exam" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c1fda018-20c1-47ea-896f-fcbb6ab608de");

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

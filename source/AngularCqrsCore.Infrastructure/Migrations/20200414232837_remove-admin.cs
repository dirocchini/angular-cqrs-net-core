using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class removeadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "Created", "CreatedBy", "DateOfBirth", "Email", "EmailConfirmed", "Gender", "Interests", "Introduction", "KnownAs", "LastActive", "LastModified", "LastModifiedBy", "LockoutEnabled", "LockoutEnd", "Login", "LookingFor", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, null, "60656206-d91b-41d3-b43b-1ebaec72ab17", null, new DateTime(2020, 4, 14, 20, 1, 54, 658, DateTimeKind.Local).AddTicks(1903), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, false, null, "admin", null, "Administrator", null, "ADMIN", "password", null, null, false, null, false, "admin" });
        }
    }
}

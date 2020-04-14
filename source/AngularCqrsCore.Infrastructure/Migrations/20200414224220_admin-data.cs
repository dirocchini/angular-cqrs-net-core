using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class admindata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Created", "Password", "UserName" },
                values: new object[] { "c7ec51ee-bd6e-4b47-90b3-8ecf0f554ced", new DateTime(2020, 4, 14, 19, 42, 19, 690, DateTimeKind.Local).AddTicks(162), "password", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Created", "Password", "UserName" },
                values: new object[] { "9f628f73-e04d-445f-9e43-3d2b6470fd9c", new DateTime(2020, 4, 13, 19, 37, 57, 874, DateTimeKind.Local).AddTicks(9820), "Yaaca3BSPqo=", null });
        }
    }
}

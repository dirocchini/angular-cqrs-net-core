using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UserDef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "9f628f73-e04d-445f-9e43-3d2b6470fd9c", new DateTime(2020, 4, 13, 19, 37, 57, 874, DateTimeKind.Local).AddTicks(9820) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "0978591a-1b29-401a-8833-dfecee324b3c", new DateTime(2020, 4, 13, 19, 0, 10, 95, DateTimeKind.Local).AddTicks(929) });
        }
    }
}

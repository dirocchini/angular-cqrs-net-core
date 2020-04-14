using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class adminnormalizedlogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Created", "NormalizedUserName" },
                values: new object[] { "60656206-d91b-41d3-b43b-1ebaec72ab17", new DateTime(2020, 4, 14, 20, 1, 54, 658, DateTimeKind.Local).AddTicks(1903), "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Created", "NormalizedUserName" },
                values: new object[] { "c7ec51ee-bd6e-4b47-90b3-8ecf0f554ced", new DateTime(2020, 4, 14, 19, 42, 19, 690, DateTimeKind.Local).AddTicks(162), null });
        }
    }
}

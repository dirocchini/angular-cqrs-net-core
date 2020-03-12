using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(type: "varchar (50)", nullable: true),
                    Login = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Login", "Name" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, 0, "droquini", "Diego Roquini" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Login", "Name" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, 0, "istanchese", "iago Stanchese" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Login", "Name" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, 0, "fvila", "Fernando Vila" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

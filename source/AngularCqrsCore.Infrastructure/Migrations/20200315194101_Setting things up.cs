using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Settingthingsup : Migration
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
                    Name = table.Column<string>(type: "varchar (50)", nullable: false),
                    Email = table.Column<string>(type: "varchar (50)", nullable: true),
                    Login = table.Column<string>(type: "varchar (50)", nullable: false),
                    Password = table.Column<string>(type: "varchar (500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "CreatedBy", "Email", "LastModified", "LastModifiedBy", "Login", "Name", "Password" },
                values: new object[] { 1, new DateTime(2020, 3, 15, 16, 41, 0, 462, DateTimeKind.Local).AddTicks(9515), 0, null, null, 0, "admin", "Administrator", "Yaaca3BSPqo=" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

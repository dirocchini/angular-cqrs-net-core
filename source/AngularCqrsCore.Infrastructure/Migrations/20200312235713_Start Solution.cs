using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class StartSolution : Migration
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
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(type: "varchar (50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Login", "Name", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 3, 12, 20, 57, 2, 441, DateTimeKind.Local).AddTicks(9184), 0, null, 0, "admin", "Administrator", "000" },
                    { 2, new DateTime(2020, 3, 12, 20, 57, 2, 442, DateTimeKind.Local).AddTicks(2207), 0, null, 0, "diego-user", "Diego", "111" },
                    { 3, new DateTime(2020, 3, 12, 20, 57, 2, 442, DateTimeKind.Local).AddTicks(2226), 0, null, 0, "iago-user", "iago", "222" },
                    { 4, new DateTime(2020, 3, 12, 20, 57, 2, 442, DateTimeKind.Local).AddTicks(2228), 0, null, 0, "fernando-user", "Fernando", "333" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

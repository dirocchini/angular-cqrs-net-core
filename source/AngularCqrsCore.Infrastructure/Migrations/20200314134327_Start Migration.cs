using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class StartMigration : Migration
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
                    Password = table.Column<string>(type: "varchar (50)", nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "CreatedBy", "Email", "LastModified", "LastModifiedBy", "Login", "Name", "Password", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, new DateTime(2020, 3, 14, 10, 43, 27, 36, DateTimeKind.Local).AddTicks(8213), 0, null, null, 0, "admin", "Administrator", "111", new byte[] { 91, 230, 146, 21, 162, 57, 245, 219, 87, 150, 127, 238, 207, 107, 75, 242, 53, 207, 95, 168, 111, 99, 198, 101, 197, 2, 205, 55, 121, 228, 94, 158, 166, 43, 56, 149, 47, 112, 65, 64, 210, 154, 217, 220, 117, 195, 196, 38, 163, 133, 16, 106, 90, 248, 214, 106, 117, 63, 83, 188, 70, 2, 19, 194, 31, 247, 88, 247, 84, 190, 121, 0, 193, 35, 148, 7, 91, 105, 202, 151, 138, 230, 153, 44, 207, 19, 139, 161, 34, 83, 45, 152, 197, 70, 40, 85, 101, 252, 252, 93, 183, 141, 66, 67, 57, 75, 143, 96, 170, 164, 195, 89, 35, 157, 57, 9, 228, 120, 153, 59, 229, 231, 69, 58, 173, 68, 98, 49 }, new byte[] { 181, 74, 43, 172, 6, 39, 169, 172, 115, 78, 193, 170, 130, 86, 121, 65, 223, 225, 190, 190, 203, 22, 30, 183, 240, 233, 48, 16, 139, 244, 49, 97, 238, 187, 166, 137, 37, 125, 51, 86, 102, 12, 209, 43, 130, 86, 152, 135, 227, 98, 82, 202, 161, 109, 66, 194, 220, 67, 6, 48, 197, 89, 204, 102 } });

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

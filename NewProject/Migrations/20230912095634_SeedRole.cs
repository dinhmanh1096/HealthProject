using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewProject.Migrations
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[] { 2, "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleID",
                keyValue: 2);
        }
    }
}

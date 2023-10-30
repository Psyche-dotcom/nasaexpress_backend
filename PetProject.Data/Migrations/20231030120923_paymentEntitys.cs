using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProject.Data.Migrations
{
    public partial class paymentEntitys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f4291fc-4662-42ba-bb9d-ec275bd068a2", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8f38afd2-761c-4048-a3cd-878ba306c16e", "1", "ADMIN", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "95b51726-e8c5-47fa-b2e2-25f1427a8b7c", "3", "RIDER", "RIDER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f4291fc-4662-42ba-bb9d-ec275bd068a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f38afd2-761c-4048-a3cd-878ba306c16e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95b51726-e8c5-47fa-b2e2-25f1427a8b7c");
        }
    }
}

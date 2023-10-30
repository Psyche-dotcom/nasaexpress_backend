using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProject.Data.Migrations
{
    public partial class tripStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e51459e-8875-4f61-9d26-f812700a7b22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b228860-4613-46b1-bc98-3494c3549462");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aca045e0-1e7e-406e-a17f-72e007e5c5dc");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserTravels",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserTravels");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3e51459e-8875-4f61-9d26-f812700a7b22", "3", "Rider", "Rider" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b228860-4613-46b1-bc98-3494c3549462", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aca045e0-1e7e-406e-a17f-72e007e5c5dc", "1", "ADMIN", "ADMIN" });
        }
    }
}

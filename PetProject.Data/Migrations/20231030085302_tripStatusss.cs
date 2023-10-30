using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProject.Data.Migrations
{
    public partial class tripStatusss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d75c782-d316-4d25-b3fe-131a6fa8e83f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70a65b39-a84e-48c6-ab76-11c5466fe368");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d86478eb-554b-4879-9b37-1c0c82570d32");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c6eb043-ec67-4dca-8622-82cfc2b2111d", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "90add292-b38e-4b89-b044-fdedf221a54c", "3", "RIDER", "RIDER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b8fb8c26-02eb-4df9-b1ed-70dc614d71a7", "1", "ADMIN", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c6eb043-ec67-4dca-8622-82cfc2b2111d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90add292-b38e-4b89-b044-fdedf221a54c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8fb8c26-02eb-4df9-b1ed-70dc614d71a7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d75c782-d316-4d25-b3fe-131a6fa8e83f", "3", "Rider", "Rider" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70a65b39-a84e-48c6-ab76-11c5466fe368", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d86478eb-554b-4879-9b37-1c0c82570d32", "1", "ADMIN", "ADMIN" });
        }
    }
}

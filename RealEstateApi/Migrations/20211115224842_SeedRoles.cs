using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateApi.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43eabe84-17da-4604-85bf-6251f8045b0b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0bdc3bfd-2972-4849-9fd1-7cedc34d0d39", "1a3da1db-17f5-4044-93fa-c319ca2da8fb", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6943c106-4fde-4773-972b-2947d0eac563", "fc694083-1b91-431d-a78e-6457089da7d6", "Moderator", "MODERATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bfa908f6-c9db-485b-a87d-f44d55b1f3bf", "eed656d6-3295-4b9c-9041-d89b60abcbbf", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bdc3bfd-2972-4849-9fd1-7cedc34d0d39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6943c106-4fde-4773-972b-2947d0eac563");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfa908f6-c9db-485b-a87d-f44d55b1f3bf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "43eabe84-17da-4604-85bf-6251f8045b0b", "993407b5-0cbe-4427-8063-a255f2c5bb53", "Admin", "ADMIN" });
        }
    }
}

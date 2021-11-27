using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateApi.Migrations
{
    public partial class testSeed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "43eabe84-17da-4604-85bf-6251f8045b0b", "993407b5-0cbe-4427-8063-a255f2c5bb53", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43eabe84-17da-4604-85bf-6251f8045b0b");
        }
    }
}

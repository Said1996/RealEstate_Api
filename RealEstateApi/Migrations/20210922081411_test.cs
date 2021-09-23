using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateApi.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "OfferType",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                table: "RealEstates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "City",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfferType",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

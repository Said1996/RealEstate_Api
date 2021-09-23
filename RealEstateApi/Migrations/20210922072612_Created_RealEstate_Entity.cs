using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateApi.Migrations
{
    public partial class Created_RealEstate_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BathroomNum",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "City",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Garden",
                table: "RealEstates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "KitchenNum",
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
                name: "Price",
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

            migrationBuilder.AddColumn<int>(
                name: "RoomNum",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SecuritySystem",
                table: "RealEstates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Space",
                table: "RealEstates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "SwimmingPool",
                table: "RealEstates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BathroomNum",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "City",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Garden",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "KitchenNum",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "OfferType",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "RoomNum",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "SecuritySystem",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Space",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "SwimmingPool",
                table: "RealEstates");
        }
    }
}

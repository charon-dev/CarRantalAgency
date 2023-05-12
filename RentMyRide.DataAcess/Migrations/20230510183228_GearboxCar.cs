using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentMyRide.DataAcess.Migrations
{
    public partial class GearboxCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gearbox",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gearbox",
                table: "Cars");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentMyRide.DataAcess.Migrations
{
    public partial class ReservationServices_RentingServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Renting_AdditionalServices_AdditionalServiceId",
                table: "Renting");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AdditionalServices_AdditionalServiceId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_AdditionalServiceId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Renting_AdditionalServiceId",
                table: "Renting");

            migrationBuilder.DropColumn(
                name: "AdditionalServiceId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "AdditionalServiceId",
                table: "Renting");

            migrationBuilder.CreateTable(
                name: "Renting_Services",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdditionalServiceId = table.Column<int>(type: "int", nullable: false),
                    RentingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renting_Services", x => x.id);
                    table.ForeignKey(
                        name: "FK_Renting_Services_AdditionalServices_AdditionalServiceId",
                        column: x => x.AdditionalServiceId,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Renting_Services_Renting_RentingId",
                        column: x => x.RentingId,
                        principalTable: "Renting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation_Services",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdditionalServiceId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation_Services", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reservation_Services_AdditionalServices_AdditionalServiceId",
                        column: x => x.AdditionalServiceId,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Services_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Renting_Services_AdditionalServiceId",
                table: "Renting_Services",
                column: "AdditionalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Renting_Services_RentingId",
                table: "Renting_Services",
                column: "RentingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Services_AdditionalServiceId",
                table: "Reservation_Services",
                column: "AdditionalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Services_ReservationId",
                table: "Reservation_Services",
                column: "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Renting_Services");

            migrationBuilder.DropTable(
                name: "Reservation_Services");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalServiceId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AdditionalServiceId",
                table: "Renting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AdditionalServiceId",
                table: "Reservations",
                column: "AdditionalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Renting_AdditionalServiceId",
                table: "Renting",
                column: "AdditionalServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Renting_AdditionalServices_AdditionalServiceId",
                table: "Renting",
                column: "AdditionalServiceId",
                principalTable: "AdditionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AdditionalServices_AdditionalServiceId",
                table: "Reservations",
                column: "AdditionalServiceId",
                principalTable: "AdditionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

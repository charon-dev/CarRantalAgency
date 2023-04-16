using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentMyRide.DataAcess.Migrations
{
    public partial class ApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Renting",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ApplicationUserId",
                table: "Reservations",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Renting_ApplicationUserId",
                table: "Renting",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ApplicationUserId",
                table: "Feedbacks",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_ApplicationUserId",
                table: "Feedbacks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Renting_AspNetUsers_ApplicationUserId",
                table: "Renting",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_ApplicationUserId",
                table: "Reservations",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_ApplicationUserId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Renting_AspNetUsers_ApplicationUserId",
                table: "Renting");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_ApplicationUserId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ApplicationUserId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Renting_ApplicationUserId",
                table: "Renting");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ApplicationUserId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Renting");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}

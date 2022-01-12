using Microsoft.EntityFrameworkCore.Migrations;

namespace Train_Reservation_Application.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeat_Reservations_ReservationId",
                table: "ReservationSeat");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "ReservationSeat",
                newName: "ReservationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeat_Reservations_ReservationsId",
                table: "ReservationSeat",
                column: "ReservationsId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeat_Reservations_ReservationsId",
                table: "ReservationSeat");

            migrationBuilder.RenameColumn(
                name: "ReservationsId",
                table: "ReservationSeat",
                newName: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeat_Reservations_ReservationId",
                table: "ReservationSeat",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

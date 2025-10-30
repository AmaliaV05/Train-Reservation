using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainReservation.Infrastructure.Migrations
{
    public partial class AddReservationSeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeat_Reservations_ReservationsId",
                table: "ReservationSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeat_Seats_SeatsId",
                table: "ReservationSeat");

            migrationBuilder.RenameColumn(
                name: "SeatsId",
                table: "ReservationSeat",
                newName: "SeatId");

            migrationBuilder.RenameColumn(
                name: "ReservationsId",
                table: "ReservationSeat",
                newName: "ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationSeat_SeatsId",
                table: "ReservationSeat",
                newName: "IX_ReservationSeat_SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeat_Reservations_ReservationId",
                table: "ReservationSeat",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeat_Seats_SeatId",
                table: "ReservationSeat",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeat_Reservations_ReservationId",
                table: "ReservationSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeat_Seats_SeatId",
                table: "ReservationSeat");

            migrationBuilder.RenameColumn(
                name: "SeatId",
                table: "ReservationSeat",
                newName: "SeatsId");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "ReservationSeat",
                newName: "ReservationsId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationSeat_SeatId",
                table: "ReservationSeat",
                newName: "IX_ReservationSeat_SeatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeat_Reservations_ReservationsId",
                table: "ReservationSeat",
                column: "ReservationsId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeat_Seats_SeatsId",
                table: "ReservationSeat",
                column: "SeatsId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainReservation.Infrastructure.Migrations
{
    public partial class UpdateSeatCalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarSeat");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "AvailableSeat",
                table: "Calendars");

            migrationBuilder.CreateTable(
                name: "SeatCalendar",
                columns: table => new
                {
                    CalendarId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    SeatAvailability = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatCalendar", x => new { x.CalendarId, x.SeatId });
                    table.ForeignKey(
                        name: "FK_SeatCalendar_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatCalendar_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatCalendar_SeatId",
                table: "SeatCalendar",
                column: "SeatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatCalendar");

            migrationBuilder.AddColumn<bool>(
                name: "Group",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AvailableSeat",
                table: "Calendars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CalendarSeat",
                columns: table => new
                {
                    CalendarsId = table.Column<int>(type: "int", nullable: false),
                    SeatsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarSeat", x => new { x.CalendarsId, x.SeatsId });
                    table.ForeignKey(
                        name: "FK_CalendarSeat_Calendars_CalendarsId",
                        column: x => x.CalendarsId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarSeat_Seats_SeatsId",
                        column: x => x.SeatsId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarSeat_SeatsId",
                table: "CalendarSeat",
                column: "SeatsId");
        }
    }
}

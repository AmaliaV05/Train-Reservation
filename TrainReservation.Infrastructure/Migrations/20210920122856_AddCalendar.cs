using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainReservation.Infrastructure.Migrations
{
    public partial class AddCalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalendarDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableSeat = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarSeat");

            migrationBuilder.DropTable(
                name: "Calendars");
        }
    }
}

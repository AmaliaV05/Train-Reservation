using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainReservation.Infrastructure.Migrations
{
    public partial class UpdateSeatAddAvailable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Seats");
        }
    }
}

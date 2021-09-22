using Microsoft.EntityFrameworkCore.Migrations;

namespace Train_Reservation_Application.Migrations
{
    public partial class UpdateSeatAddPropertyGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Group",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Seats");
        }
    }
}

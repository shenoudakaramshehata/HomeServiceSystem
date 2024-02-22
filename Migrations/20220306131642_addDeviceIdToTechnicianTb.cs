using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class addDeviceIdToTechnicianTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "Technician",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Technician");
        }
    }
}

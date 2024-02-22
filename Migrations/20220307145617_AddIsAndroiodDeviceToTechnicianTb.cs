using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class AddIsAndroiodDeviceToTechnicianTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAndroiodDevice",
                table: "Technician",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIosDevice",
                table: "Technician",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAndroiodDevice",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "IsIosDevice",
                table: "Technician");
        }
    }
}

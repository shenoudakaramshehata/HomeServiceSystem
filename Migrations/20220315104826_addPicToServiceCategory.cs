using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class addPicToServiceCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pic",
                table: "ServiceCategory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pic",
                table: "ServiceCategory");
        }
    }
}

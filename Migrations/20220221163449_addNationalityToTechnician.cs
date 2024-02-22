using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class addNationalityToTechnician : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Technician_NationalityId",
                table: "Technician",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technician_Nationality_NationalityId",
                table: "Technician",
                column: "NationalityId",
                principalTable: "Nationality",
                principalColumn: "NationalityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technician_Nationality_NationalityId",
                table: "Technician");

            migrationBuilder.DropIndex(
                name: "IX_Technician_NationalityId",
                table: "Technician");
        }
    }
}

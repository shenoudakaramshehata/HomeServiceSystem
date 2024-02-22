using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class EditRequestTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosd",
                table: "Request",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssueDescription",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SparePartsCost",
                table: "Request",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SparePartsDescription",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechDescription",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechDiagnosis",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechFixes",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechnicianId",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VisitCost",
                table: "Request",
                type: "real",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_TechnicianId",
                table: "Request",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Technician_TechnicianId",
                table: "Request",
                column: "TechnicianId",
                principalTable: "Technician",
                principalColumn: "TechnicianId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Technician_TechnicianId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_TechnicianId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "IsClosd",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "IssueDescription",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "SparePartsCost",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "SparePartsDescription",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "TechDescription",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "TechDiagnosis",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "TechFixes",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "VisitCost",
                table: "Request");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class addServiceCategoryToRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceCategoryId",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_ServiceCategoryId",
                table: "Request",
                column: "ServiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_ServiceCategory_ServiceCategoryId",
                table: "Request",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategory",
                principalColumn: "ServiceCategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_ServiceCategory_ServiceCategoryId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_ServiceCategoryId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ServiceCategoryId",
                table: "Request");
        }
    }
}

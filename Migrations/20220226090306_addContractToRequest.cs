using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class addContractToRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_ContractId",
                table: "Request",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Contract_ContractId",
                table: "Request",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Contract_ContractId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_ContractId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Request");
        }
    }
}

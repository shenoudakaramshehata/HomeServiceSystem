using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class addContractTypeIdToContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractTypeId",
                table: "Contract",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractTypeId",
                table: "Contract",
                column: "ContractTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ContractType_ContractTypeId",
                table: "Contract",
                column: "ContractTypeId",
                principalTable: "ContractType",
                principalColumn: "ContractTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_ContractType_ContractTypeId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_ContractTypeId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "ContractTypeId",
                table: "Contract");
        }
    }
}

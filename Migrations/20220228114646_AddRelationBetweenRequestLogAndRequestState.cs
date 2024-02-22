using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class AddRelationBetweenRequestLogAndRequestState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestStateId",
                table: "RequestLog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RequestLog_RequestStateId",
                table: "RequestLog",
                column: "RequestStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLog_RequestState_RequestStateId",
                table: "RequestLog",
                column: "RequestStateId",
                principalTable: "RequestState",
                principalColumn: "RequestStateId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLog_RequestState_RequestStateId",
                table: "RequestLog");

            migrationBuilder.DropIndex(
                name: "IX_RequestLog_RequestStateId",
                table: "RequestLog");

            migrationBuilder.DropColumn(
                name: "RequestStateId",
                table: "RequestLog");
        }
    }
}

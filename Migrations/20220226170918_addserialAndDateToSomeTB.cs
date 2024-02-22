using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class addserialAndDateToSomeTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "VDate",
                table: "RequestLog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptSerial",
                table: "Receipt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractSerial",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VDate",
                table: "RequestLog");

            migrationBuilder.DropColumn(
                name: "ReceiptSerial",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "ContractSerial",
                table: "Contract");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class removeVDateInReceiptTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VDate",
                table: "Receipt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "VDate",
                table: "Receipt",
                type: "date",
                nullable: true);
        }
    }
}

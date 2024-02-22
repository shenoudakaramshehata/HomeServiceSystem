using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class addVDateInReceiptTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "VDate",
                table: "Receipt",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VDate",
                table: "Receipt");
        }
    }
}

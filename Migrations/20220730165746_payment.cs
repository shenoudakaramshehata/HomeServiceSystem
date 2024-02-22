using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Auth",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Request",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentID",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "Request",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ref",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackID",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TranID",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isPaid",
                table: "Request",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "payment_type",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auth",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PaymentID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Ref",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "TrackID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "TranID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "isPaid",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "payment_type",
                table: "Request");
        }
    }
}

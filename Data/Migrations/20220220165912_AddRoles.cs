using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Data.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c6e5f3e-e3cb-4978-88c1-38144a2034b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a301e986-6a05-4889-80fb-4baaa314a9c7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c5daa8fe-f585-43be-85cc-fa55c9be95ad", "609508ad-6fed-4168-a4f6-50aaa83c1f32", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "645a9379-8101-4039-8d83-083a1eeba728", "8cea5fc5-fe56-453a-ac0d-cc25abcc04cf", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8ee9994-23da-43d9-8f5e-8388d4094bc9", "b9bc0cfe-b7b4-40fa-a35d-fd723a104529", "Technician", "TECHNICIAN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "645a9379-8101-4039-8d83-083a1eeba728");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5daa8fe-f585-43be-85cc-fa55c9be95ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8ee9994-23da-43d9-8f5e-8388d4094bc9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a301e986-6a05-4889-80fb-4baaa314a9c7", "3e391e2a-d935-4ce7-9a84-67ec27597ffb", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c6e5f3e-e3cb-4978-88c1-38144a2034b9", "e83a487a-e4e6-4cc7-b49e-49da1662d474", "Trainer", "TRAINER" });
        }
    }
}

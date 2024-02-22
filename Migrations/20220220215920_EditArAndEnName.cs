using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeService.Migrations
{
    public partial class EditArAndEnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitGroupTLEn",
                table: "UnitGroup",
                newName: "UnitGroupTlEn");

            migrationBuilder.RenameColumn(
                name: "UnitGroupTLAr",
                table: "UnitGroup",
                newName: "UnitGroupTlAr");

            migrationBuilder.RenameColumn(
                name: "ServiceCategoryTLEn",
                table: "ServiceCategory",
                newName: "ServiceCategoryTlEn");

            migrationBuilder.RenameColumn(
                name: "ServiceCategoryTLAr",
                table: "ServiceCategory",
                newName: "ServiceCategoryTlAr");

            migrationBuilder.RenameColumn(
                name: "ServiceTLEn",
                table: "Service",
                newName: "ServiceTlEn");

            migrationBuilder.RenameColumn(
                name: "ServiceTLAr",
                table: "Service",
                newName: "ServiceTlAr");

            migrationBuilder.RenameColumn(
                name: "ReceiptServiceTLEn",
                table: "ReceiptService",
                newName: "ReceiptServiceTlEn");

            migrationBuilder.RenameColumn(
                name: "ReceiptServiceTLAr",
                table: "ReceiptService",
                newName: "ReceiptServiceTlAr");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodTLEn",
                table: "PaymentMethod",
                newName: "PaymentMethodTlEn");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodTLAr",
                table: "PaymentMethod",
                newName: "PaymentMethodTlAr");

            migrationBuilder.RenameColumn(
                name: "NationalityIdTLEn",
                table: "Nationality",
                newName: "NationalityTlEn");

            migrationBuilder.RenameColumn(
                name: "NationalityIdTLAr",
                table: "Nationality",
                newName: "NationalityTlAr");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitGroupTlEn",
                table: "UnitGroup",
                newName: "UnitGroupTLEn");

            migrationBuilder.RenameColumn(
                name: "UnitGroupTlAr",
                table: "UnitGroup",
                newName: "UnitGroupTLAr");

            migrationBuilder.RenameColumn(
                name: "ServiceCategoryTlEn",
                table: "ServiceCategory",
                newName: "ServiceCategoryTLEn");

            migrationBuilder.RenameColumn(
                name: "ServiceCategoryTlAr",
                table: "ServiceCategory",
                newName: "ServiceCategoryTLAr");

            migrationBuilder.RenameColumn(
                name: "ServiceTlEn",
                table: "Service",
                newName: "ServiceTLEn");

            migrationBuilder.RenameColumn(
                name: "ServiceTlAr",
                table: "Service",
                newName: "ServiceTLAr");

            migrationBuilder.RenameColumn(
                name: "ReceiptServiceTlEn",
                table: "ReceiptService",
                newName: "ReceiptServiceTLEn");

            migrationBuilder.RenameColumn(
                name: "ReceiptServiceTlAr",
                table: "ReceiptService",
                newName: "ReceiptServiceTLAr");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodTlEn",
                table: "PaymentMethod",
                newName: "PaymentMethodTLEn");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodTlAr",
                table: "PaymentMethod",
                newName: "PaymentMethodTLAr");

            migrationBuilder.RenameColumn(
                name: "NationalityTlEn",
                table: "Nationality",
                newName: "NationalityIdTLEn");

            migrationBuilder.RenameColumn(
                name: "NationalityTlAr",
                table: "Nationality",
                newName: "NationalityIdTLAr");
        }
    }
}

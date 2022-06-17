using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoiceManagement.Migrations.Invoice
{
    public partial class _modify_model_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InvoiceNo",
                table: "Customers",
                type: "int",
                maxLength: 40,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNo",
                table: "Customers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 40);
        }
    }
}

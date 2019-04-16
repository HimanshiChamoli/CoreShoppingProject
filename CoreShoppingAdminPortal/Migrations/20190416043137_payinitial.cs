using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreShoppingAdminPortal.Migrations
{
    public partial class payinitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Zip",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Payments");

            migrationBuilder.AlterColumn<string>(
                name: "Zip",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}

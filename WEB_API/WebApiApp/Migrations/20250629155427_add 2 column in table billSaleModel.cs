using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiApp.Migrations
{
    /// <inheritdoc />
    public partial class add2columnintablebillSaleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "BillSaleModel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReceiveAmount",
                table: "BillSaleModel",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "BillSaleModel");

            migrationBuilder.DropColumn(
                name: "ReceiveAmount",
                table: "BillSaleModel");
        }
    }
}

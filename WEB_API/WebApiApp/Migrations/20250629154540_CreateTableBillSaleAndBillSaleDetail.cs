using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableBillSaleAndBillSaleDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillSaleModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillSaleModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillSaleDetailModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BillSaleId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    Qty = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillSaleDetailModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillSaleDetailModel_BillSaleModel_BillSaleId",
                        column: x => x.BillSaleId,
                        principalTable: "BillSaleModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillSaleDetailModel_BookModel_BookId",
                        column: x => x.BookId,
                        principalTable: "BookModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillSaleDetailModel_BillSaleId",
                table: "BillSaleDetailModel",
                column: "BillSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_BillSaleDetailModel_BookId",
                table: "BillSaleDetailModel",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillSaleDetailModel");

            migrationBuilder.DropTable(
                name: "BillSaleModel");
        }
    }
}

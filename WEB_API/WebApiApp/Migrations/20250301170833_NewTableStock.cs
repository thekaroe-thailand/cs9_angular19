using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiApp.Migrations
{
    /// <inheritdoc />
    public partial class NewTableStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "BookModel",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockModel_BookModel_BookId",
                        column: x => x.BookId,
                        principalTable: "BookModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookModel_StockId",
                table: "BookModel",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockModel_BookId",
                table: "StockModel",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookModel_StockModel_StockId",
                table: "BookModel",
                column: "StockId",
                principalTable: "StockModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookModel_StockModel_StockId",
                table: "BookModel");

            migrationBuilder.DropTable(
                name: "StockModel");

            migrationBuilder.DropIndex(
                name: "IX_BookModel_StockId",
                table: "BookModel");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "BookModel");
        }
    }
}

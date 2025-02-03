using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiApp.Migrations
{
    /// <inheritdoc />
    public partial class NewModelAndAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "BookModel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PublisherModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookModel_PublisherId",
                table: "BookModel",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookModel_PublisherModel_PublisherId",
                table: "BookModel",
                column: "PublisherId",
                principalTable: "PublisherModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookModel_PublisherModel_PublisherId",
                table: "BookModel");

            migrationBuilder.DropTable(
                name: "PublisherModel");

            migrationBuilder.DropIndex(
                name: "IX_BookModel_PublisherId",
                table: "BookModel");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "BookModel");
        }
    }
}

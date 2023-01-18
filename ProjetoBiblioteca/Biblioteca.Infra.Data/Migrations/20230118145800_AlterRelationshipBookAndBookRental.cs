using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterRelationshipBookAndBookRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BooksRents_LivroId",
                table: "BooksRents");

            migrationBuilder.CreateIndex(
                name: "IX_BooksRents_LivroId",
                table: "BooksRents",
                column: "LivroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BooksRents_LivroId",
                table: "BooksRents");

            migrationBuilder.CreateIndex(
                name: "IX_BooksRents_LivroId",
                table: "BooksRents",
                column: "LivroId",
                unique: true);
        }
    }
}

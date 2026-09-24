using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoPecas.Migrations
{
    /// <inheritdoc />
    public partial class UniqueSku : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Sku",
                table: "Produtos",
                column: "Sku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Produtos_Sku",
                table: "Produtos");
        }
    }
}

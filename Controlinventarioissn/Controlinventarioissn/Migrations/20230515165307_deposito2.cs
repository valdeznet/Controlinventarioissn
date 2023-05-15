using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Controlinventarioissn.Migrations
{
    /// <inheritdoc />
    public partial class deposito2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Depositos_Name",
                table: "Depositos",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Depositos_Name",
                table: "Depositos");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");
        }
    }
}

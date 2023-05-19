using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Controlinventarioissn.Migrations
{
    /// <inheritdoc />
    public partial class productcateory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Equipamientos");

            migrationBuilder.AddColumn<string>(
                name: "NumeroRfid",
                table: "Equipamientos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroRfid",
                table: "Equipamientos");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Equipamientos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

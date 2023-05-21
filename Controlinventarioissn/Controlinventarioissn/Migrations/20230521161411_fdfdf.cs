using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Controlinventarioissn.Migrations
{
    /// <inheritdoc />
    public partial class fdfdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipamientoDepositos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipamientoId = table.Column<int>(type: "int", nullable: false),
                    DepositoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipamientoDepositos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipamientoDepositos_Depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Depositos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipamientoDepositos_Equipamientos_EquipamientoId",
                        column: x => x.EquipamientoId,
                        principalTable: "Equipamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipamientoDepositos_DepositoId",
                table: "EquipamientoDepositos",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipamientoDepositos_EquipamientoId_DepositoId",
                table: "EquipamientoDepositos",
                columns: new[] { "EquipamientoId", "DepositoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipamientoDepositos");
        }
    }
}

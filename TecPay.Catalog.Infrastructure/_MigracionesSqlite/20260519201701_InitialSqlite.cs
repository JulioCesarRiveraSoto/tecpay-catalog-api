using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TecPay.Catalog.Infrastructure._MigracionesSqlite
{
    /// <inheritdoc />
    public partial class InitialSqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoriaNombre = table.Column<string>(type: "TEXT", nullable: false),
                    FechaCreacionUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaActualizacionUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FechaBajaUtc = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductoNombre = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ProductoSKU = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    FK_IDCategoria = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductoPrecio = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ProductoStock = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoDescripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    FechaCreacionUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaActualizacionUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FechaBajaUtc = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_FK_IDCategoria",
                        column: x => x.FK_IDCategoria,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producto_FK_IDCategoria",
                table: "Producto",
                column: "FK_IDCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_ProductoSKU",
                table: "Producto",
                column: "ProductoSKU",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}

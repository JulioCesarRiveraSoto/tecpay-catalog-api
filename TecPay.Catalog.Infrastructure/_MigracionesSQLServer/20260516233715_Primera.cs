using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TecPay.Catalog.Infrastructure._MigracionesSQLServer
{
    /// <inheritdoc />
    public partial class Primera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacionUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacionUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaBajaUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoNombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProductoSKU = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FK_IDCategoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoPrecio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductoStock = table.Column<int>(type: "int", nullable: false),
                    ProductoDescripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaCreacionUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacionUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaBajaUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
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

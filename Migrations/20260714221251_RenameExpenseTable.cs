using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OndeFoi.Migrations
{
    /// <inheritdoc />
    public partial class RenameExpenseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Categoria_CategoriaId",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Usuario_UsuarioId",
                table: "Gastos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gastos",
                table: "Gastos");

            migrationBuilder.RenameTable(
                name: "Gastos",
                newName: "Gasto");

            migrationBuilder.RenameIndex(
                name: "IX_Gastos_UsuarioId",
                table: "Gasto",
                newName: "IX_Gasto_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Gastos_CategoriaId",
                table: "Gasto",
                newName: "IX_Gasto_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gasto",
                table: "Gasto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Categoria_CategoriaId",
                table: "Gasto",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Usuario_UsuarioId",
                table: "Gasto",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Categoria_CategoriaId",
                table: "Gasto");

            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Usuario_UsuarioId",
                table: "Gasto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gasto",
                table: "Gasto");

            migrationBuilder.RenameTable(
                name: "Gasto",
                newName: "Gastos");

            migrationBuilder.RenameIndex(
                name: "IX_Gasto_UsuarioId",
                table: "Gastos",
                newName: "IX_Gastos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Gasto_CategoriaId",
                table: "Gastos",
                newName: "IX_Gastos_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gastos",
                table: "Gastos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Categoria_CategoriaId",
                table: "Gastos",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Usuario_UsuarioId",
                table: "Gastos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

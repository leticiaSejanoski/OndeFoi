using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OndeFoi.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarRelacionamentoUsuarioCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Categoria",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_UsuarioId",
                table: "Categoria",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Usuario_UsuarioId",
                table: "Categoria",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Usuario_UsuarioId",
                table: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_UsuarioId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Categoria");
        }
    }
}

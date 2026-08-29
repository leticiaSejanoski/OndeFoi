using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OndeFoi.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirUsuarioIdRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Usuario_usuarioId",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "usuarioId",
                table: "RefreshToken",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_usuarioId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Usuario_UsuarioId",
                table: "RefreshToken",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Usuario_UsuarioId",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "RefreshToken",
                newName: "usuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UsuarioId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_usuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Usuario_usuarioId",
                table: "RefreshToken",
                column: "usuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

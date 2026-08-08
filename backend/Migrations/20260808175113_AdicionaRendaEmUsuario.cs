using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OndeFoi.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaRendaEmUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Renda",
                table: "Usuario",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Renda",
                table: "Usuario");
        }
    }
}

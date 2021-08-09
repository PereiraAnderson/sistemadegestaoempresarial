using Microsoft.EntityFrameworkCore.Migrations;

namespace SGE.Context.Migrations
{
    public partial class ManyRequerimentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Requerimento_UsuarioId",
                table: "Requerimento");

            migrationBuilder.CreateIndex(
                name: "IX_Requerimento_UsuarioId",
                table: "Requerimento",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Requerimento_UsuarioId",
                table: "Requerimento");

            migrationBuilder.CreateIndex(
                name: "IX_Requerimento_UsuarioId",
                table: "Requerimento",
                column: "UsuarioId",
                unique: true);
        }
    }
}

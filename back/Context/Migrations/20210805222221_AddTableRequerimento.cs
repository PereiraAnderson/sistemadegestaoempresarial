using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGE.Context.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AddTableRequerimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requerimento",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTimeOffset>(nullable: false),
                    DataModificacao = table.Column<DateTimeOffset>(nullable: true),
                    Justificativa = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<long>(nullable: false),
                    PontoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requerimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requerimento_Ponto_PontoId",
                        column: x => x.PontoId,
                        principalTable: "Ponto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requerimento_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requerimento_PontoId",
                table: "Requerimento",
                column: "PontoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requerimento_UsuarioId",
                table: "Requerimento",
                column: "UsuarioId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requerimento");
        }
    }
}

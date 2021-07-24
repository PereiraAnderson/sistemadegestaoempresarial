using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGE.Context.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTimeOffset>(nullable: false),
                    DataModificacao = table.Column<DateTimeOffset>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    Perfil = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ponto",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTimeOffset>(nullable: false),
                    DataModificacao = table.Column<DateTimeOffset>(nullable: true),
                    Data = table.Column<DateTimeOffset>(nullable: false),
                    UsuarioId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ponto_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Ativo", "CPF", "DataCriacao", "DataModificacao", "Login", "Nome", "Perfil", "Senha" },
                values: new object[] { 1L, true, "00000000000", new DateTimeOffset(new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), null, "admin@sge.com", "Usuário Administrador SGE", 1, "$2a$11$llgzqoCJoRHxX00w3rqdwO8yN1/xvq1w.UERLsN4KjTFo/2Dvk3mS" });

            migrationBuilder.CreateIndex(
                name: "IX_Ponto_UsuarioId",
                table: "Ponto",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ponto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}

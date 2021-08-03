using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGE.Context.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AddingColumnTarefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tarefa",
                table: "Ponto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tarefa",
                table: "Ponto");
        }
    }
}

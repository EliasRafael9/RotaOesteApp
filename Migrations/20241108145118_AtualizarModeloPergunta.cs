using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRotaOeste.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarModeloPergunta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoPergunta",
                table: "Perguntas",
                newName: "Descricao");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Data",
                table: "Perguntas",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Perguntas");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Perguntas",
                newName: "TipoPergunta");
        }
    }
}

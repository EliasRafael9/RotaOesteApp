using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoRotaOeste.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespostasItem_Conversas_ConversaIdConversa",
                table: "RespostasItem");

            migrationBuilder.DropForeignKey(
                name: "FK_RespostasItem_Perguntas_PerguntaIdPergunta",
                table: "RespostasItem");

            migrationBuilder.DropTable(
                name: "Conversas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RespostasItem",
                table: "RespostasItem");

            migrationBuilder.DropIndex(
                name: "IX_RespostasItem_ConversaIdConversa",
                table: "RespostasItem");

            migrationBuilder.DropIndex(
                name: "IX_RespostasItem_PerguntaIdPergunta",
                table: "RespostasItem");

            migrationBuilder.DropColumn(
                name: "IdRespostaItem",
                table: "RespostasItem");

            migrationBuilder.DropColumn(
                name: "ConversaIdConversa",
                table: "RespostasItem");

            migrationBuilder.DropColumn(
                name: "IdConversa",
                table: "RespostasItem");

            migrationBuilder.DropColumn(
                name: "PerguntaIdPergunta",
                table: "RespostasItem");

            migrationBuilder.RenameTable(
                name: "RespostasItem",
                newName: "RespostaItem");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "RespostaItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RespostaItem",
                table: "RespostaItem",
                column: "IdPergunta");

            migrationBuilder.CreateIndex(
                name: "IX_RespostaItem_UsuarioId",
                table: "RespostaItem",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_RespostaItem_AspNetUsers_UsuarioId",
                table: "RespostaItem",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RespostaItem_Perguntas_IdPergunta",
                table: "RespostaItem",
                column: "IdPergunta",
                principalTable: "Perguntas",
                principalColumn: "IdPergunta",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespostaItem_AspNetUsers_UsuarioId",
                table: "RespostaItem");

            migrationBuilder.DropForeignKey(
                name: "FK_RespostaItem_Perguntas_IdPergunta",
                table: "RespostaItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RespostaItem",
                table: "RespostaItem");

            migrationBuilder.DropIndex(
                name: "IX_RespostaItem_UsuarioId",
                table: "RespostaItem");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "RespostaItem");

            migrationBuilder.RenameTable(
                name: "RespostaItem",
                newName: "RespostasItem");

            migrationBuilder.AddColumn<int>(
                name: "IdRespostaItem",
                table: "RespostasItem",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ConversaIdConversa",
                table: "RespostasItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdConversa",
                table: "RespostasItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PerguntaIdPergunta",
                table: "RespostasItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RespostasItem",
                table: "RespostasItem",
                column: "IdRespostaItem");

            migrationBuilder.CreateTable(
                name: "Conversas",
                columns: table => new
                {
                    IdConversa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteIdCliente = table.Column<int>(type: "integer", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdCliente = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversas", x => x.IdConversa);
                    table.ForeignKey(
                        name: "FK_Conversas_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RespostasItem_ConversaIdConversa",
                table: "RespostasItem",
                column: "ConversaIdConversa");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasItem_PerguntaIdPergunta",
                table: "RespostasItem",
                column: "PerguntaIdPergunta");

            migrationBuilder.CreateIndex(
                name: "IX_Conversas_ClienteIdCliente",
                table: "Conversas",
                column: "ClienteIdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_RespostasItem_Conversas_ConversaIdConversa",
                table: "RespostasItem",
                column: "ConversaIdConversa",
                principalTable: "Conversas",
                principalColumn: "IdConversa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RespostasItem_Perguntas_PerguntaIdPergunta",
                table: "RespostasItem",
                column: "PerguntaIdPergunta",
                principalTable: "Perguntas",
                principalColumn: "IdPergunta",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

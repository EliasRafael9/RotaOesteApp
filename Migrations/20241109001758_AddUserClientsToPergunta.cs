using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRotaOeste.Migrations
{
    /// <inheritdoc />
    public partial class AddUserClientsToPergunta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PerguntaUsuarios",
                columns: table => new
                {
                    PerguntaIdPergunta = table.Column<int>(type: "integer", nullable: false),
                    UserClientsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntaUsuarios", x => new { x.PerguntaIdPergunta, x.UserClientsId });
                    table.ForeignKey(
                        name: "FK_PerguntaUsuarios_AspNetUsers_UserClientsId",
                        column: x => x.UserClientsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerguntaUsuarios_Perguntas_PerguntaIdPergunta",
                        column: x => x.PerguntaIdPergunta,
                        principalTable: "Perguntas",
                        principalColumn: "IdPergunta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerguntaUsuarios_UserClientsId",
                table: "PerguntaUsuarios",
                column: "UserClientsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerguntaUsuarios");
        }
    }
}

namespace ProjetoRotaOeste.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RespostaItem
{
    [Key, ForeignKey("Pergunta")]
    public int IdPergunta { get; set; } // Primary key and foreign key
    public required string TextoResposta { get; set; }
    public int Ordem { get; set; }

    public required Pergunta Pergunta { get; set; } // Navegação para Pergunta
    public required Usuario Usuario { get; set; }    // Navegação para Usuario
}

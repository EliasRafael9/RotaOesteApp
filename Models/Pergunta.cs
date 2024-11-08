namespace ProjetoRotaOeste.Models;

public class Pergunta
{
    public int IdPergunta { get; set; }
    public required string TextoPergunta { get; set; }
    public required string TipoPergunta { get; set; } // Opcional
}

namespace ProjetoRotaOeste.Models;

public class Pergunta
{
    public int IdPergunta { get; set; }
    public required string TextoPergunta { get; set; }
    public required string Descricao { get; set; }
    public required DateOnly Data { get; set; }
    public required ICollection<Usuario> UserClients { get; set; }
    public RespostaItem RespostaItem { get; set; }
    //public required string TipoPergunta { get; set; } // Opcional
}

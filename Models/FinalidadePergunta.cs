namespace ProjetoRotaOeste.Models
{
    public class FinalidadePergunta
    {
        public int IdFinalidade { get; set; }
        public required string Descricao { get; set; }
        public int IdPergunta { get; set; }
        
        public virtual required Pergunta Pergunta { get; set; } // Navegação para Pergunta
    }
}

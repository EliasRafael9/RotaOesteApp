namespace ProjetoRotaOeste.Models
{
    public class RespostaItem
    {
        public int IdRespostaItem { get; set; }
        public int IdConversa { get; set; }
        public int IdPergunta { get; set; }
        public required string TextoResposta { get; set; }
        public int Ordem { get; set; }
        
        public required Conversa Conversa { get; set; } // Navegação para Conversa
        public required Pergunta Pergunta { get; set; } // Navegação para Pergunta
    }
}

namespace ProjetoRotaOeste.DTOs
{
    public class PerguntaResponseDto
    {
        public int IdPergunta { get; set; }
        public string TextoPergunta { get; set; }
        public string Descricao { get; set; }
        public DateOnly Data { get; set; }
        public List<string> ClienteNomes { get; set; }
        public List<string> ClienteEmails { get; set; }
    }
}
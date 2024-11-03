namespace ProjetoRotaOeste.Dtos
{
    public class MensagemRecebidaDto
    {
        public int IdCliente { get; set; }
        public int IdPergunta { get; set; }
        public required string TextoResposta { get; set; }
    }
}

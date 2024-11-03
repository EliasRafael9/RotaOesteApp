namespace ProjetoRotaOeste.Models
{
    public class Conversa
    {
        public int IdConversa { get; set; }
        public int IdCliente { get; set; }
        public required string Status { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; }
        
        public virtual required Cliente Cliente { get; set; } // Navegação para o Cliente
    }
}

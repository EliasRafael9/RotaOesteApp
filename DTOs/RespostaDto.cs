
namespace ProjetoRotaOeste.DTOs;

public class RespostaDto
{
    public int IdPergunta { get; set; }
    
    public string EmailUsuario { get; set; }
    public required string TextoResposta { get; set; }
}
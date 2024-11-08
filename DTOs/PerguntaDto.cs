using System.ComponentModel.DataAnnotations;

namespace ProjetoRotaOeste.DTOs;
public class PerguntaDto
{
    [Required(ErrorMessage = "O texto da pergunta é obrigatório.")]
    public required string TextoPergunta { get; set; }

    [Required(ErrorMessage = "A descrição da pergunta é obrigatória.")]
    public required string Descricao { get; set; }
    
    [Required(ErrorMessage = "A data da pergunta é obrigatória.")]
    public required DateOnly Data { get; set; }
}

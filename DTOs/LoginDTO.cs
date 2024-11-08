using System.ComponentModel.DataAnnotations;

namespace ProjetoRotaOeste.DTOs;
public class LoginDTO
{
    [Required(ErrorMessage = "Forneça um email.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Forneça uma senha.")]
    public string Senha { get; set; }
}
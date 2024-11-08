using System.ComponentModel.DataAnnotations;

namespace ProjetoRotaOeste.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required]
    public required string Nome { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Senha { get; set; } // Aqui você deve armazenar a senha de forma segura (hashed)
}

using Microsoft.AspNetCore.Identity;

namespace ProjetoRotaOeste.Models;

public class Usuario : IdentityUser
{
    public required string Nome { get; set; }
}

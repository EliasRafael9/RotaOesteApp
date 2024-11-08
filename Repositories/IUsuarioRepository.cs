using System.Threading.Tasks;
using ProjetoRotaOeste.Models;

public interface IUsuarioRepository
{
    Task<Usuario> GetUsuarioByEmail(string email);
    Task<Usuario> AddUsuario(Usuario usuario);
}

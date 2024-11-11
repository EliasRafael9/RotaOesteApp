using System.Threading.Tasks;
using ProjetoRotaOeste.Models;

public interface IUsuarioRepository
{
    public Task<Usuario> GetUserByEmailAsync(string email);
    Task<IEnumerable<Usuario>> GetAllClientesAsync();
}

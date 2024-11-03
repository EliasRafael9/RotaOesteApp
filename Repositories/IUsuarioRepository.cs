using System.Threading.Tasks;

public interface IUsuarioRepository
{
    Task<Usuario> GetUsuarioByEmail(string email);
    Task<Usuario> AddUsuario(Usuario usuario);
}

using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetoRotaOeste.Models;

namespace ProjetoRotaOeste.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetAllClientesAsync();
        Task AddClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(int id);
    }
}

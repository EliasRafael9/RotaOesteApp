using ProjetoRotaOeste.DTOs;
using ProjetoRotaOeste.Models;

public interface IPerguntaRepository
{
    Task<Pergunta> AdicionarPerguntaAsync(PerguntaDto perguntaDto);
    Task<IEnumerable<Pergunta>> GetPerguntasByUserEmailAsync(string email);
}

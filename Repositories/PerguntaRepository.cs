using ProjetoRotaOeste.DTOs;
using ProjetoRotaOeste.Models;
using Microsoft.EntityFrameworkCore;

public class PerguntaRepository : IPerguntaRepository
{
    private readonly AppDbContext _context;

    public PerguntaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pergunta> AdicionarPerguntaAsync(PerguntaDto perguntaDto)
    {
        var users = await _context.Users
            .Where(u => perguntaDto.UserClientEmails.Contains(u.Email))
            .ToListAsync();

        var pergunta = new Pergunta
        {
            TextoPergunta = perguntaDto.TextoPergunta,
            Descricao = perguntaDto.Descricao,
            Data = perguntaDto.Data,
            UserClients = users
        };

        _context.Perguntas.Add(pergunta);
        await _context.SaveChangesAsync();
        return pergunta;
    }

    public async Task<IEnumerable<Pergunta>> GetPerguntasPorUsuarioAsync(string email)
    {
        return await _context.Perguntas
            .Include(p => p.UserClients)
            .Where(p => p.UserClients.Any(u => u.Email == email))
            .ToListAsync();
    }
}
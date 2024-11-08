using ProjetoRotaOeste.DTOs;
using ProjetoRotaOeste.Models;

public class PerguntaRepository : IPerguntaRepository
{
    private readonly AppDbContext _context;

    public PerguntaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pergunta> AdicionarPerguntaAsync(PerguntaDto perguntaDto)
    {
        var pergunta = new Pergunta
        {
            TextoPergunta = perguntaDto.TextoPergunta,
            Descricao = perguntaDto.Descricao,
            Data = perguntaDto.Data
        };

        _context.Perguntas.Add(pergunta);
        await _context.SaveChangesAsync();
        return pergunta;
    }
}
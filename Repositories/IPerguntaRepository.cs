using ProjetoRotaOeste.Models;

public interface IPerguntaRepository
{
    Task<Pergunta> AdicionarPerguntaAsync(PerguntaDto perguntaDto);
}

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
            TipoPergunta = perguntaDto.TipoPergunta
        };

        _context.Perguntas.Add(pergunta);
        await _context.SaveChangesAsync();
        return pergunta;
    }
}

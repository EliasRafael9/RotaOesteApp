using Microsoft.AspNetCore.Mvc;
using ProjetoRotaOeste.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProjetoRotaOeste.Controllers{

[ApiController]
[Route("api/[controller]")]
public class PerguntaController : ControllerBase
{
     private readonly AppDbContext _context;

        public PerguntaController(AppDbContext context)
        {
            _context = context;
        }
    private readonly IPerguntaRepository _perguntaRepository;

    public PerguntaController(IPerguntaRepository perguntaRepository)
    {
        _perguntaRepository = perguntaRepository;
    }
    // GET: api/pergunta
     [HttpGet]
    public async Task<ActionResult<IEnumerable<Pergunta>>> GetPerguntas()
    {
         return await _context.Perguntas.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarPergunta([FromBody] PerguntaDto perguntaDto)
    {
        if (string.IsNullOrEmpty(perguntaDto.TextoPergunta))
        {
            return BadRequest("O texto da pergunta é obrigatório.");
        }

        var pergunta = await _perguntaRepository.AdicionarPerguntaAsync(perguntaDto);
        return CreatedAtAction(nameof(AdicionarPergunta), new { id = pergunta.IdPergunta }, pergunta);
    }
 
}
}
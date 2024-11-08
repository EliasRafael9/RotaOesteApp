using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using ProjetoRotaOeste.Models;
using ProjetoRotaOeste.Repositories;
using ProjetoRotaOeste.DTOs;

namespace ProjetoRotaOeste.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerguntaController : ControllerBase
{
     private readonly AppDbContext _context;
     private readonly IPerguntaRepository _perguntaRepository;

    public PerguntaController(AppDbContext context, IPerguntaRepository perguntaRepository)
    {
        _context = context;
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
        var pergunta = await _perguntaRepository.AdicionarPerguntaAsync(perguntaDto);
        return CreatedAtAction(nameof(AdicionarPergunta), new { id = pergunta.IdPergunta }, pergunta);
    }
 
}
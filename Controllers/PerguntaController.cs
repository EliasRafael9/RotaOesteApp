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
    public async Task<ActionResult<IEnumerable<PerguntaResponseDto>>> GetPerguntas()
    {
        var perguntas = await _context.Perguntas
            .Include(p => p.UserClients)
            .ToListAsync();

        var response = perguntas.Select(p => new PerguntaResponseDto
        {
            IdPergunta = p.IdPergunta,
            TextoPergunta = p.TextoPergunta,
            Descricao = p.Descricao,
            Data = p.Data,
            ClienteNomes = p.UserClients.Select(u => u.Nome).ToList(),
            ClienteEmails = p.UserClients.Select(u => u.Email).ToList()
        });

        return Ok(response);
    }

    [HttpPost("adicionar")]
    public async Task<IActionResult> AdicionarPergunta([FromBody] PerguntaDto perguntaDto)
    {
        var pergunta = await _perguntaRepository.AdicionarPerguntaAsync(perguntaDto);
        return CreatedAtAction(nameof(AdicionarPergunta), new { id = pergunta.IdPergunta }, pergunta);
    }
 
}
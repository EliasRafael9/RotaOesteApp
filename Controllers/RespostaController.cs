using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ProjetoRotaOeste.Models;
using ProjetoRotaOeste.Repositories;
using ProjetoRotaOeste.DTOs;

namespace ProjetoRotaOeste.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RespostaController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IUsuarioRepository _usuarioRepository;

    public RespostaController(AppDbContext context, IUsuarioRepository usuarioRepository)
    {
        _context = context;
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost("adicionar")]
    public async Task<IActionResult> AdicionarResposta([FromBody] RespostaDto respostaDto)
    {
        // Verifica se a pergunta existe
        var pergunta = await _context.Perguntas.FindAsync(respostaDto.IdPergunta);
        if (pergunta == null)
        {
            return NotFound("Pergunta não encontrada.");
        }

        // Verifica se o usuário existe
        var usuario = await _usuarioRepository.GetUserByEmailAsync(respostaDto.EmailUsuario);
        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        var respostaExistente = await _context.RespostaItems
            .FirstOrDefaultAsync(r => r.IdPergunta == respostaDto.IdPergunta);

        if (respostaExistente != null)
        {
            return BadRequest("Uma resposta para esta pergunta já existe.");
        }

        // Cria a nova resposta
        var respostaItem = new RespostaItem
        {
            IdPergunta = respostaDto.IdPergunta,
            Pergunta = pergunta,
            Usuario = usuario,
            TextoResposta = respostaDto.TextoResposta,
            Ordem = 0 
        };

        _context.RespostaItems.Add(respostaItem);
        await _context.SaveChangesAsync();

        return Ok("Resposta adicionada com sucesso.");
    }

    [HttpGet("porPergunta/{idPergunta}")]
    public async Task<IActionResult> GetRespostaPorPergunta(int idPergunta)
    {
        // Get the single response for the specific question
        var resposta = await _context.RespostaItems
            .Include(r => r.Usuario)
            .FirstOrDefaultAsync(r => r.IdPergunta == idPergunta);

        if (resposta == null)
        {
            return NotFound("Nenhuma resposta encontrada para esta pergunta.");
        }

        var response = new
        {
            resposta.IdPergunta,
            resposta.TextoResposta,
            UsuarioNome = resposta.Usuario.Nome,
            UsuarioEmail = resposta.Usuario.Email
        };

        return Ok(response);
    }
}
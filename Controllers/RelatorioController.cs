using Microsoft.AspNetCore.Mvc;
using ProjetoRotaOeste.Models;
using ProjetoRotaOeste.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoRotaOeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioController(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        // GET: api/Relatorio/MensagensPorTipo
        [HttpGet("MensagensPorTipo")]
        public async Task<ActionResult<IEnumerable<RelatorioMensagensPorTipo>>> ObterRelatorioMensagensPorTipo(
            [FromQuery] DateTime? dataInicio,
            [FromQuery] DateTime? dataFim,
            [FromQuery] int? idCliente)
        {
            try
            {
                // Chamando o repositório para obter o relatório
                var relatorio = await _relatorioRepository.ObterTotalMensagensPorTipoAsync(dataInicio, dataFim, idCliente);

                // Verifica se o relatório está vazio
                if (relatorio == null || relatorio.Count == 0)
                {
                    return NotFound("Nenhum dado encontrado para os filtros aplicados.");
                }

                // Retorna o resultado com status 200 OK
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                // Log de erro (caso necessário, pode-se usar um logger)
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }
    }
}

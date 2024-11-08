/*
using Microsoft.EntityFrameworkCore;
using ProjetoRotaOeste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRotaOeste.Repositories;
public class RelatorioRepository : IRelatorioRepository
{
    private readonly AppDbContext _context;

    public RelatorioRepository(AppDbContext context)
    {
        _context = context;
    }

    
    public async Task<List<RelatorioMensagensPorTipo>> GerarRelatorioMensagensPorTipoAsync(DateTime? dataInicio, DateTime? dataFim, int? idCliente)
    {
        var query = _context.RespostasItem.AsQueryable();

        // Filtra por cliente, se fornecido
        if (idCliente.HasValue)
        {
            query = query.Where(r => r.Conversa.IdCliente == idCliente.Value);
        }

        // Filtra por data de início e fim, se fornecido
        if (dataInicio.HasValue)
        {
            query = query.Where(r => r.Conversa.DataInicio >= dataInicio.Value);
        }

        if (dataFim.HasValue)
        {
            query = query.Where(r => r.Conversa.DataInicio <= dataFim.Value);
        }

        // Juntando a tabela de respostas com a tabela de perguntas e agrupando por tipo
        return await query
            .Join(_context.Perguntas,
                r => r.IdPergunta,
                p => p.IdPergunta,
                (r, p) => new { r, p })
            .GroupBy(rp => rp.p.TipoPergunta)
            .Select(g => new RelatorioMensagensPorTipo
            {
                TipoPergunta = g.Key,
                TotalMensagens = g.Count()
            })
            .ToListAsync();
    }

    public async Task<List<RelatorioMensagensPorTipo>> ObterTotalMensagensPorTipoAsync(DateTime? dataInicio, DateTime? dataFim, int? idCliente)
    {
        // Reutiliza o método GerarRelatorioMensagensPorTipoAsync, se necessário.
        return await GerarRelatorioMensagensPorTipoAsync(dataInicio, dataFim, idCliente);
    }

    public Task<int> ObterTotalPerguntasPorTipoAsync()
    {
        // Implementação de exemplo (você pode adaptar conforme necessário)
        return _context.Perguntas
            .CountAsync();
    }
    
}
*/

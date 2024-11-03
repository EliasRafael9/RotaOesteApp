using ProjetoRotaOeste.Dtos;
using ProjetoRotaOeste.Models;
using Microsoft.EntityFrameworkCore;

public class MensagemService : IMensagemService
{
    private readonly AppDbContext _context;

    public MensagemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task ProcessarMensagemRecebida(MensagemRecebidaDto mensagem)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == mensagem.IdCliente);
        if (cliente != null)
        {
                    // Criar nova conversa
                    var conversa = new Conversa
                    {
                        IdCliente = mensagem.IdCliente,
                        Status = "em atendimento",
                        DataInicio = DateTime.UtcNow,
                        Cliente = cliente
                    };
                

            _context.Conversas.Add(conversa);
            await _context.SaveChangesAsync();

            // Supondo que a pergunta já esteja cadastrada
            var pergunta = await _context.Perguntas.FirstOrDefaultAsync(p => p.IdPergunta == mensagem.IdPergunta);
            
            if (pergunta != null)
            {
                // Armazenar a resposta
                var respostaItem = new RespostaItem
                {
                    IdConversa = conversa.IdConversa,
                    IdPergunta = pergunta.IdPergunta,
                    TextoResposta = mensagem.TextoResposta,
                    Ordem = 1, // Defina a ordem da resposta conforme necessário
                    Conversa = conversa,
                    Pergunta = pergunta
                };

                _context.RespostasItem.Add(respostaItem);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task EnviarMensagem(int idCliente, string texto)
    {
        // Lógica para enviar mensagem via API do WhatsApp
        // Implementar a chamada para a API aqui
        
    }
}

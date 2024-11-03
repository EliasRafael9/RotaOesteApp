using ProjetoRotaOeste.Dtos;
public interface IMensagemService
{
    Task ProcessarMensagemRecebida(MensagemRecebidaDto mensagem);
    Task EnviarMensagem(int idCliente, string texto);
}
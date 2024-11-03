using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ProjetoRotaOeste.Hubs
{
    public class NotificationHub : Hub
    {
        // Método para enviar notificações a um usuário específico
        public async Task EnviarNotificacao(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceberNotificacao", message);
        }
    }
}

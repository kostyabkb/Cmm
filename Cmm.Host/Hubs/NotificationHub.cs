using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Cmm.Host.Hubs
{
    /// <summary>
    /// Хаб взаимодействия с клиентами.
    /// </summary>
    public class NotificationHub : Hub
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Task.</returns>
        public async Task SendNotify(string message)
        {
            await Clients.All.SendAsync("Notify", message);
        }
    }
}

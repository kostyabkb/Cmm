using System;
using System.Threading.Tasks;
using Cmm.Host.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Cmm.Host.Services
{
    ///<inheritdoc/>
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly string methodName = "Notify";
        private readonly string updateDevice = "UpdateDevice";

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="hubContext">Хаб.</param>
        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task SendDevicesUpdateNotify()
        {
            await hubContext.Clients.All.SendAsync(methodName, updateDevice);
        }
    }
}

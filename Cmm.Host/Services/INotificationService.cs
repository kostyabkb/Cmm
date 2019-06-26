using System;
using System.Threading.Tasks;

namespace Cmm.Host.Services
{
    /// <summary>
    /// Сервис отправки нотификаций.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Отправить уведомление обновления списка устройств.
        /// </summary>
        Task SendDevicesUpdateNotify();
    }
}

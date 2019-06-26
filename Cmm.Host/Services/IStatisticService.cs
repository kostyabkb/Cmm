using System;
using System.Threading.Tasks;
using Cmm.Contracts;

namespace Cmm.Host.Services
{
    /// <summary>
    /// Сервис обработки статистики.
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Сохранения устройства и событий.
        /// </summary>
        /// <param name="device">Устройство.</param>
        Task Save(DeviceRequest device);
    }
}

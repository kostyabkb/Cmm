using System.Collections.Generic;
using Cmm.Contracts;
using Cmm.Host.Model;

namespace Cmm.Host.Services
{
    /// <summary>
    /// Сервис обработки статистики.
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Получить статистику.
        /// </summary>
        /// <returns>Коллекция статистики в виде DeviceResponse.</returns>
        List<DeviceResponse> GetStatistic();

        /// <summary>
        /// Сохранить(или обновить) статистику device.
        /// </summary>
        /// <param name="device">Статистика в виде DeviceStatistic.</param>
        void Save(DeviceStatistic device);
    }
}

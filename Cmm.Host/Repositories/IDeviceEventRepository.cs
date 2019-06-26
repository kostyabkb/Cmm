using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Host.Model;

namespace Cmm.Host.Repositories
{
    /// <summary>
    /// Репозиторий events.
    /// </summary>
    public interface IDeviceEventRepository
    {
        /// <summary>
        /// Добавить событие.
        /// </summary>
        /// <param name="newDeviceEvent">Событие.</param>
        /// <param name="deviceId">Id устройства.</param>
        Task Add(DeviceEvent newDeviceEvent);

        /// <summary>
        /// Получить.
        /// </summary>
        /// <param name="deviceId">Id устройства.</param>
        /// <returns>Список событий.</returns>
        Task<List<EventDescription>> GetForDevice(Guid deviceId);

        /// <summary>
        /// Удаление всех событий для устройства.
        /// </summary>
        /// <param name="deviceId">Идентификатор.</param>
        /// <returns>Task.</returns>
        Task DeleteForDevice(Guid deviceId);
    }
}

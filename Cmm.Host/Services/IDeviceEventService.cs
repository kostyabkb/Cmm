using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Contracts;

namespace Cmm.Host.Services
{
    /// <summary>
    /// Сервис событий.
    /// </summary>
    public interface IDeviceEventService
    {
        /// <summary>
        /// Получить.
        /// </summary>
        /// <returns>Список событий.</returns>
        Task<List<DeviceEventResponse>> GetForDevice(Guid deviceId);


        /// <summary>
        /// Удаление всех событий для устройства.
        /// </summary>
        /// <param name="deviceId">Идентификатор.</param>
        /// <returns>Task.</returns>
        Task DeleteForDevice(Guid deviceId);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Contracts;

namespace Cmm.Host.Services
{
    /// <summary>
    /// Сервис обработки статистики.
    /// </summary>
    public interface IDevicesService
    {
        /// <summary>
        /// Получить статистику.
        /// </summary>
        /// <returns>Коллекция статистики в виде DeviceResponse.</returns>
        Task<List<DeviceResponse>> Get();

        /// <summary>
        /// Получить статистику определенного устройства по id.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Устройство.</returns>
        Task<DeviceResponse> GetById(Guid id);

        /// <summary>
        /// Переименовать устройство по id.
        /// </summary>
        /// <param name="device">Новое имя устройства.</param>
        /// <returns>Task.</returns>
        Task Rename(UpdateDeviceRequest device);
    }
}

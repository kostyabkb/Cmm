using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Host.Model;

namespace Cmm.Host.Repositories
{
    /// <summary>
    /// Репозиторий устройств.
    /// </summary>
    public interface IDeviceRepository
    {
        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="device">Устройство.</param>
        Task Add(Device device);

        /// <summary>
        /// Получить список.
        /// </summary>
        /// <returns>Список Device.</returns>
        Task<List<Device>> Get();

        /// <summary>
        /// Получить объект по Id.
        /// </summary>
        /// <param name="deviceId">Id.</param>
        /// <returns>Найденный объект или null.</returns>
        Task<Device> GetById(Guid deviceId);

        /// <summary>
        /// Обновить запись.
        /// </summary>
        /// <param name="device">Устройство.</param>
        Task Update(Device device);
    }
}

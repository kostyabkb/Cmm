using System;
using System.Collections.Generic;
using Cmm.Host.Model;

namespace Cmm.Host.Repositories
{
    /// <summary>
    /// Репозиторий device.
    /// </summary>
    public interface IDeviceRepository
    {
        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="device">Устройство.</param>
        void Add(Device device);

        /// <summary>
        /// Получить список.
        /// </summary>
        /// <returns>Список Device.</returns>
        List<Device> Get();

        /// <summary>
        /// Получить объект по Id.
        /// </summary>
        /// <param name="deviceId">Id.</param>
        /// <returns>Найденный объект или null.</returns>
        Device GetById(Guid deviceId);

        /// <summary>
        /// Обновить запись.
        /// </summary>
        /// <param name="device">Устройство.</param>
        void Update(Device device);
    }
}

using System;
using System.Collections.Generic;
using Cmm.Contracts;
using Cmm.Host.Model;

namespace Cmm.Host.Services
{
    /// <summary>
    /// Репозиторий device.
    /// </summary>
    interface IRepository
    {
        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="device">Девайс.</param>
        void Add(Device device);

        /// <summary>
        /// Получить список.
        /// </summary>
        /// <returns>Список Device.</returns>
        List<Device> Get();

        /// <summary>
        /// Обновить запись.
        /// </summary>
        /// <param name="device">Устройство.</param>
        /// <param name="index">Номер в коллекции.</param>
        void Update(Device devicex, Device device);

        /// <summary>
        /// Получить объект по Id.
        /// </summary>
        /// <param name="deviceId">Id.</param>
        /// <returns>Найденный объект или null.</returns>
        Device GetById(Guid deviceid);
    }
}

using System;
using Cmm.Host.Repositories;

namespace Cmm.Host.UOW
{
    /// <summary>
    /// Паттерн UOW.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Отправить.
        /// </summary>
        void Commit();

        /// <summary>
        /// Получить device репозиторий.
        /// </summary>
        /// <returns>Репозиторий устройств.</returns>
        IDeviceRepository GetDeviceRepository();

        /// <summary>
        /// Получить event репозиторий.
        /// </summary>
        /// <returns>Репозиторий событий.</returns>
        IDeviceEventRepository GetEventRepository();

        /// <summary>
        /// Получить deviceEvent репозиторий.
        /// </summary>
        /// <returns>Event репозиторий.</returns>
        IEventRepository GetDeviceEventRepository();

        /// <summary>
        /// Откатить.
        /// </summary>
        void Rollback();
    }
}

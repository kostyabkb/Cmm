using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Host.Model;

namespace Cmm.Host.Repositories
{
    /// <summary>
    /// Репозиторий описания событий.
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Добавление нового описания события.
        /// </summary>
        /// <param name="deviceEvent">Описание события.</param>
        /// <returns>Task.</returns>
        Task Add(String eventName);

        /// <summary>
        /// Получить все описания событий.
        /// </summary>
        /// <returns>Список описаний событий.</returns>
        Task<List<Event>> Get();

        /// <summary>
        /// Получение события по имени.
        /// </summary>
        /// <param name="name">Имя события.</param>
        /// <returns>Список</returns>
        Task<Event> GetByName(string name);

        /// <summary>
        /// Обновить описания события.
        /// </summary>
        /// <param name="event">Описание события.</param>
        /// <returns></returns>
        Task Update(Event @event);
    }
}

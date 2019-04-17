using System;
using System.Collections.Generic;
using Cmm.Host.Model;

namespace Cmm.Host.Repositories
{
    /// <summary>
    /// Репозиторий events.
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="newEvent">Событие.</param>
        void Add(Event newEvent);

        /// <summary>
        /// Получить.
        /// </summary>
        /// <returns>Список событий.</returns>
        List<Event> Get();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cmm.Contracts;
using Cmm.Host.Model;

namespace Cmm.Host.Services
{
    /// <summary>
    /// Сервис описаний событий.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Получить описания.
        /// </summary>
        /// <returns>Коллекция описаний.</returns>
        Task<List<Event>> Get();

        /// <summary>
        /// Добавить описание.
        /// </summary>
        /// <param name="event">Описание события.</param>
        /// <returns>Task.</returns>
        Task Update(Event @event);
    }
}

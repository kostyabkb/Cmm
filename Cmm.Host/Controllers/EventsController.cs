using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Contracts;
using Cmm.Host.Model;
using Cmm.Host.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cmm.Host.Controllers
{
    /// <summary>
    /// Контроллер cправочника событий.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventService eventService;
        private readonly ILogger logger = Log.ForContext<EventsController>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="eventService">Сервис описания событий.</param>
        public EventsController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        /// <summary>
        /// Добавление описания.
        /// </summary>
        /// <param name="newEvent">Описание события.</param>
        /// <returns>Task.</returns>
        [HttpPut]
        public async Task UpdateEvents([FromBody] EventRequest newEvent)
        {
            logger.Debug("Данные описаний событий валидированы и добавляются в базу.");

            newEvent.Description = newEvent.Description.Trim();

            try
            {
                await eventService.Update(newEvent.Adapt<Event>());
            }
            catch (Exception e)
            {
                logger.Error($"Error, transaction failed: {e.Message}");
                throw;
            }
        }

        /// <summary>
        /// Получение описаний.
        /// </summary>
        /// <returns>Коллекция описаний.</returns>
        [HttpGet]
        public async Task<List<Event>> GetEvents()
        {
            logger.Debug("Данные описаний событий отправляются по запросу.");

            return await eventService.Get();
        }
    }
}

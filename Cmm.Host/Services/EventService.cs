using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Host.Model;
using Cmm.Host.Repositories;
using Cmm.Host.UOW;

namespace Cmm.Host.Services
{
    /// <inheritdoc/>
    public class EventService : IEventService
    {
        private readonly IUnitOfWorkFactory factory;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="factory">Фабрика uow.</param>
        public EventService(IUnitOfWorkFactory factory)
        {
            this.factory = factory;
        }

        public async Task<List<Event>> Get()
        {
            using (IUnitOfWork uow = factory.Create())
            {
                IEventRepository deviceEventRepository = uow.GetDeviceEventRepository();

                return await deviceEventRepository.Get();
            }
        }

        public async Task Update(Event @event)
        {
            using (IUnitOfWork uow = factory.Create())
            {
                IEventRepository deviceEventRepository = uow.GetDeviceEventRepository();

                if (CheckMatched(@event, deviceEventRepository))
                {
                    return;
                }

                await deviceEventRepository.Update(@event);

                uow.Commit();
            }
        }

        private bool CheckMatched(Event newEvent, IEventRepository eventRepository)
        {
            Event existEvent = eventRepository.GetByName(newEvent.Name).Result;

            if (existEvent.Description.Equals(newEvent.Description) && existEvent.Level.Equals(newEvent.Level))
            {
                return true;
            }

            return false;
        }
    }
}

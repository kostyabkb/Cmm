using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Contracts;
using Cmm.Host.Model;
using Cmm.Host.Repositories;
using Cmm.Host.UOW;
using Mapster;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cmm.Host.Services
{
    /// <inheritdoc/>
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWorkFactory uowFactory;
        private readonly INotificationService notificationService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="uowFactory">Фабрика uow.</param>
        /// /// <param name="notificationService">Сервис нотификаций.</param>

        public StatisticService(IUnitOfWorkFactory uowFactory, INotificationService notificationService)
        {
            this.uowFactory = uowFactory;
            this.notificationService = notificationService;
        }

        public async Task Save(DeviceRequest device)
        {
            using (IUnitOfWork uow = uowFactory.Create())
            {
                IDeviceRepository devicesRepository = uow.GetDeviceRepository();
                IDeviceEventRepository deviceEventsRepository = uow.GetEventRepository();
                IEventRepository eventRepository = uow.GetDeviceEventRepository();

                await SaveOrUpdateDevice(device, devicesRepository);

                await SaveEvents(device.DeviceEvents.Adapt<List<DeviceEvent>>(), device.Id, deviceEventsRepository, eventRepository);

                uow.Commit();
            }
        }

        private static async Task SaveEvents(List<DeviceEvent> events, Guid id, IDeviceEventRepository deviceEventRepository, IEventRepository eventRepository)
        {
            foreach (DeviceEvent newEvent in events)
            {
                newEvent.DeviceId = id;
                await deviceEventRepository.Add(newEvent);

                if (!CheckExist(eventRepository, newEvent))
                {
                    await eventRepository.Add(newEvent.Name);
                }
            }
        }

        private async Task SaveOrUpdateDevice(DeviceRequest device, IDeviceRepository deviceRepository)
        {
            if (await deviceRepository.GetById(device.Id) == null)
            {
                await deviceRepository.Add(device.Adapt<Device>());
                await notificationService.SendDevicesUpdateNotify();
            }
            else
            {
                await deviceRepository.Update(device.Adapt<Device>());
            }
        }

        private static bool CheckExist(IEventRepository eventRepository, DeviceEvent newDeviceEvent)
        {
            if (eventRepository.GetByName(newDeviceEvent.Name).Result == null)
            {
                return false;
            }
            return true;
        }
    }
}

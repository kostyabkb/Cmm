using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Contracts;
using Cmm.Host.Model;
using Cmm.Host.Repositories;
using Cmm.Host.UOW;
using Mapster;
using Serilog;

namespace Cmm.Host.Services
{
    /// <inheritdoc/>
    public class DeviceEventService : IDeviceEventService
    {
        private readonly IUnitOfWorkFactory factory;
        private readonly ILogger logger = Log.ForContext<DeviceEventService>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="factory">Фабрика uow.</param>
        public DeviceEventService(IUnitOfWorkFactory factory)
        {
            this.factory = factory;
        }

        public async Task<List<DeviceEventResponse>> GetForDevice(Guid deviceId)
        {
            using (IUnitOfWork uow = factory.Create())
            {
                IDeviceEventRepository deviceEventsRepository = uow.GetEventRepository();

                List<EventDescription> response = await deviceEventsRepository.GetForDevice(deviceId);
                return response.Adapt<List<DeviceEventResponse>>();
            }
        }

        public async Task DeleteForDevice(Guid deviceId)
        {
            using (IUnitOfWork uow = factory.Create())
            {
                IDeviceEventRepository deviceEventsRepository = uow.GetEventRepository();

                await deviceEventsRepository.DeleteForDevice(deviceId);
                uow.Commit();
            }
        }
    }
}

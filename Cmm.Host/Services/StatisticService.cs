using System;
using System.Collections.Generic;
using Cmm.Contracts;
using Cmm.Host.Model;
using Cmm.Host.Repositories;
using Mapster;
using Serilog;

namespace Cmm.Host.Services
{
    /// <inheritdoc/>
    public class StatisticService : IStatisticService
    {
        private readonly IDeviceRepository deviceDeviceRepository;
        private readonly ILogger logger = Log.ForContext<StatisticService>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="deviceDeviceRepository">Репозиторий.</param>
        public StatisticService(IDeviceRepository deviceDeviceRepository)
        {
            this.deviceDeviceRepository = deviceDeviceRepository;
        }

        public List<DeviceResponse> GetStatistic()
        {
            return deviceDeviceRepository.Get()
                .Adapt<List<Device>, List<DeviceResponse>>();
        }

        public void Save(DeviceStatistic device)
        {
            if (deviceDeviceRepository.GetById(device.Id) == null)
            {
                deviceDeviceRepository.Add(device
                    .Adapt<DeviceStatistic, Device>());
            }
            else
            {
                deviceDeviceRepository.Update(device
                    .Adapt<DeviceStatistic, Device>());
            }
        }
    }
}

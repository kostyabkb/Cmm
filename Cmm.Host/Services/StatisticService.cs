using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Cmm.Contracts;
using Cmm.Host.Model;
using Serilog;

namespace Cmm.Host.Services
{
    /// <inheritdoc/>
    public class StatisticService : IStatisticService
    {
        private readonly DeviceRepository deviceRepository;
        private readonly ILogger logger = Log.ForContext<StatisticService>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="deviceRepository">Репозиторий.</param>
        public StatisticService(DeviceRepository deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        public List<DeviceResponse> GetStatistic()
        {
            return deviceRepository.Get()
                .ConvertAll(new Converter<Device, DeviceResponse>(Convert));
        }

        public void Save(DeviceStatistic device)
        {
            var result = deviceRepository.GetById(device.Id);
            if (result != null)
            {
                deviceRepository.Update(result, Convert(device));
            }
            else
            {
                deviceRepository.Add(Convert(device));
            }
        }

        private DeviceResponse Convert(Device device)
        {
            return new DeviceResponse
            {
                Name = device.Name,
                Os = device.Os,
                Version = device.Version
            };
        }

        private Device Convert(DeviceStatistic device)
        {
            return new Device
            {
                Id = device.Id,
                Name = device.Name,
                Os = device.Os,
                Version = device.Version
            };
        }
    }
}

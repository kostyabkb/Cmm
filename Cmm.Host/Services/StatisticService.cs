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
        private readonly IRepository deviceRepository;
        private readonly ILogger logger = Log.ForContext<StatisticService>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="deviceRepository">Репозиторий.</param>
        public StatisticService(IRepository deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        public List<DeviceResponse> GetStatistic()
        {
            return deviceRepository.Get().Select(x => new DeviceResponse
            {
                Name = x.Name,
                Os = x.Os,
                Version = x.Version
            }).ToList();
        }

        public void Save(DeviceStatistic device)
        {
            if (!deviceRepository.Get().Any(x => x.Id == device.Id))
            {
                deviceRepository.Add(Convert(device));
            }
            else
            {
                deviceRepository.Update(Convert(device));
            }
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

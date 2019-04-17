using System;
using System.Collections.Generic;
using System.Linq;
using Cmm.Host.Model;

namespace Cmm.Host.Repositories
{
    /// <inheritdoc/>
    public class InMemoryDeviceRepository : IDeviceRepository
    {
        private readonly List<Device> items;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="items">Коллекция device.</param>
        public InMemoryDeviceRepository()
        {
            items = new List<Device>();
        }

        public void Add(Device device)
        {
            items.Add(device);
        }

        public List<Device> Get()
        {
            return items;
        }

        public Device GetById(Guid deviceId)
        {
            return items.FirstOrDefault(x => x.Id == deviceId);
        }

        public void Update(Device device)
        {
            for (var i = 0; i < items.Count(); i++)
            {
                if (items[i].Id == device.Id)
                {
                    items[i] = device;
                }
            }
        }
    }
}

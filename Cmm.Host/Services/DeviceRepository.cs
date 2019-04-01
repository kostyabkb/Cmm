using System;
using System.Collections.Generic;
using System.Linq;
using Cmm.Host.Model;

namespace Cmm.Host.Services
{
    /// <inheritdoc/>
    public class DeviceRepository : IRepository
    {
        private readonly List<Device> items;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="items">Коллекция device.</param>
        public DeviceRepository(List<Device> items)
        {
            if (items.Any())
            {
                this.items = items;
            }
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
            foreach (Device item in items)
            {
                if (item.Id == deviceId)
                {
                    return item;
                }
            }

            return null;
        }

        public void Update(Device oldDevice, Device device)
        {
            int index = items.IndexOf(oldDevice);
            items.Insert(index, device);
        }
    }
}

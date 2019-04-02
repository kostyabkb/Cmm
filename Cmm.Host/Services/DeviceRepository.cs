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
        public DeviceRepository()
        {
            items = new List<Device>
            {
                new Device
                {
                    Id = new Guid(),
                    Name = "ddd",
                    Os = "34",
                    Version = "df"
                }
            };
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

        public void Update(Device device)
        {
            for (int i = 0; i < items.Count(); i++)
            {
                if (items[i].Id == device.Id)
                {
                    items[i] = device;
                }
            }
        }
    }
}

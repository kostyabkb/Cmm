using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Cmm.Contracts;
using Cmm.Host.Model;
using Cmm.Host.Repositories;
using Cmm.Host.UOW;
using Mapster;
using Microsoft.AspNetCore.Mvc;

using Serilog;

namespace Cmm.Host.Services
{
    /// <inheritdoc/>
    public class DevicesService : IDevicesService
    {
        private readonly IUnitOfWorkFactory factory;
        private readonly ILogger logger = Log.ForContext<DevicesService>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="factory">Фабрика uow.</param>
        public DevicesService(IUnitOfWorkFactory factory)
        {
            this.factory = factory;
        }

        public async Task<List<DeviceResponse>> Get()
        {
            using (IUnitOfWork uow = factory.Create())
            {
                IDeviceRepository devicesRepository = uow.GetDeviceRepository();
                List<Device> response = await devicesRepository.Get();
                return response.Adapt<List<Device>, List<DeviceResponse>>();
            }
        }

        public async Task<DeviceResponse> GetById(Guid id)
        {
            using (IUnitOfWork uow = factory.Create())
            {
                IDeviceRepository devicesRepository = uow.GetDeviceRepository();
                Device response = await devicesRepository.GetById(id);
                return response.Adapt<Device, DeviceResponse>();
            }
        }

        public async Task Rename(UpdateDeviceRequest device)
        {
            using (IUnitOfWork uow = factory.Create())
            {
                IDeviceRepository devicesRepository = uow.GetDeviceRepository();
                Device existDevice = await devicesRepository.GetById(device.Id);

                if (existDevice == null)
                {
                    throw new Exception("Запись с таким id не найдена");
                }

                existDevice.Name = device.Name;
                await devicesRepository.Update(existDevice);
                uow.Commit();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Contracts;
using Cmm.Host.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cmm.Host.Controllers
{
    /// <summary>
    /// Контроллер для работы со статистикой устройств.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private readonly IDevicesService devicesService;
        private readonly ILogger logger = Log.ForContext<DevicesController>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="devicesService">Сервис статистики.</param>
        public DevicesController(IDevicesService devicesService)
        {
            this.devicesService = devicesService;
        }

        /// <summary>
        /// Получение коллекции DeviceResponse.
        /// </summary>
        /// <returns>Список информации об устройствах.</returns>
        [HttpGet]
        public async Task<List<DeviceResponse>> GetDeviceList()
        {
            logger.Debug("Данные устройств отправляются по запросу.");

            return await devicesService.Get();
        }

        /// <summary>
        /// Получение DeviceResponse.
        /// </summary>
        /// <returns>Информация об устройстве.</returns>
        [HttpGet("{id:guid}")]
        public async Task<DeviceResponse> GetDeviceById(Guid id)
        {
            logger.Debug($"Данные устройства {id} отправляются по запросу.");

            return await devicesService.GetById(id);
        }

        [HttpPut]
        public async Task RenameDevice([FromBody] UpdateDeviceRequest device)
        {
            logger.Debug($"Устройство {device.Id} будет переименовано в {device.Name}");

            await devicesService.Rename(device);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmm.Contracts;
using Cmm.Host.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cmm.Host.Controllers
{
    /// <summary>
    /// Контроллер событий устройств.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesEventsController : Controller
    {
        private readonly IDeviceEventService deviceEventService;
        private readonly ILogger logger = Log.ForContext<DevicesEventsController>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="deviceEventService">Сервис событий.</param>
        public DevicesEventsController(IDeviceEventService deviceEventService)
        {
            this.deviceEventService = deviceEventService;
        }

        [HttpDelete("{id:guid}")]
        public async Task<StatusCodeResult> DeleteEventsById(Guid id)
        {
            logger.Debug($"Запрос на удаление событий id: {id}");

            try
            {
                await deviceEventService.DeleteForDevice(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception e)
            {
                logger.Error($"Error, transaction failed: {e.Message}");
                throw;
            }
        }

        /// <summary>
        /// Получение.
        /// </summary>
        /// <param name="id">Id устройства.</param>
        /// <returns>Коллекция событий.</returns>
        [HttpGet("{id:guid}")]
        public async Task<List<DeviceEventResponse>> GetEventsForDevice(Guid id)
        {
            logger.Debug($"Данные событий для {id} отправляются по запросу.");

            return await deviceEventService.GetForDevice(id);
        }
    }
}

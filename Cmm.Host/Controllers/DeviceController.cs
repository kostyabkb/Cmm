using System;
using System.Collections.Generic;
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
    public class DeviceController : Controller
    {
        private readonly ILogger logger = Log.ForContext<DeviceController>();
        private readonly IStatisticService statisticService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="statisticService">Сервис статистики.</param>
        public DeviceController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        /// <summary>
        /// Получение коллекции DeviceResponse.
        /// </summary>
        /// <returns>Список информации об устройствах.</returns>
        [EnableCors]
        [HttpGet]
        public List<DeviceResponse> GetDeviceList()
        {
            logger.Debug("Данные отправляются по запросу.");

            return statisticService.GetStatistic();
        }
    }
}

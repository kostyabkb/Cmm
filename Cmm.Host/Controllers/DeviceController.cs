using System;
using System.Collections.Generic;
using Cmm.Contracts;
using Cmm.Host.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Web;

namespace Cmm.Host.Controllers
{
    /// <summary>
    /// Контроллер.
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
        /// Get контроллер.
        /// </summary>
        /// <returns>Коллекцию элементов DeviceResponse.</returns>
        [HttpGet]
        public List<DeviceResponse> GetDeviceList()
        {
            logger.Debug("Данные отправляются по запросу.");

            return statisticService.GetStatistic();
        }
    }
}

using System;
using Cmm.Contracts;
using Cmm.Host.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cmm.Host.Controllers
{
    /// <summary>
    /// Контроллер для работы с устройствами.
    /// </summary>
    [Route("api/[controller]")]
    public class StatisticController : Controller
    {
        private readonly ILogger logger = Log.ForContext<StatisticController>();
        private readonly IStatisticService statisticService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="statisticService">Сервис статистики.</param>
        public StatisticController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        /// <summary>
        /// Пост контроллер.
        /// </summary>
        /// <param name="device">Статистика устройства.</param>
        [HttpPost]
        public void AddDevice([FromBody] DeviceStatistic device)
        {
            logger.Debug("Были получены и валидированы данные.");
            statisticService.Save(device);
        }
    }
}

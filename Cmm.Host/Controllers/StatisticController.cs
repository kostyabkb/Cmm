using Cmm.Host.Model;
using Cmm.Host.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cmm.Host.Controllers
{
    /// <summary>
    /// Контроллер.
    /// </summary>
    [Route("api/[controller]")]
    public class StatisticController : Controller
    {
        private readonly IStatisticService statisticService;
        private readonly ILogger logger = Log.ForContext<StatisticController>();

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
        /// <param name="device">Девайс.</param>
        [HttpPost]
        public void AddDevice([FromBody]DeviceStatistic device)
        {        
                logger.Debug("Были получены и валидированы данные.");
                statisticService.Save(device);
        }
    }
}

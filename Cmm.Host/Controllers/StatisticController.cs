using Cmm.Host.Model;
using Cmm.Host.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cmm.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : Controller
    {
        private readonly IStatisticService statisticService;
        private readonly ILogger logger = Log.ForContext<StatisticController>();

        public StatisticController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        // POST api/<controller>
        [HttpPost]
        public void AddDevice([FromBody]DeviceStatistic device)
        {        
                logger.Debug("Были получены и валидированы данные.");
                statisticService.Save(device);
        }
    }
}

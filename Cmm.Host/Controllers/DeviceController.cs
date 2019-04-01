using System;
using System.Collections.Generic;
using Cmm.Contracts;
using Cmm.Host.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cmm.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : Controller
    {
        private readonly ILogger logger = Log.ForContext<DeviceController>();
        private readonly IStatisticService statisticService;

        public DeviceController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        [HttpGet]
        public IEnumerable<DeviceResponse> GetList()
        {
            logger.Debug("Данные отправляются по запросу.");

            return statisticService.GetStatistic();
        }
    }
}

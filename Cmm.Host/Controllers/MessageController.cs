using System;
using System.Threading.Tasks;
using Cmm.Host.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Cmm.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public MessageController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task Post([FromBody]string msg)
        {
            await _hubContext.Clients.All.SendAsync("notify", new[] { msg });
        }
    }
}

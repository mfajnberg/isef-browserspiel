using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Services;

namespace web_api.Controllers
{
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly INotification _notification;
        public NotificationController(IConfiguration configuration, INotification notification)
        {
            _configuration = configuration;
            _notification =   notification;
        }

        [HttpPost("api/notification/sendto")]
        public async Task<ActionResult<string>> SendTo([FromBody] UserDto request)
        {
            _notification.SendTo("Hallo Welt", "Test - Nachricht", "stephan@ranger8.de");
            return Ok("test");
        }

    }
}

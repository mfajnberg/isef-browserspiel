using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.DTOs;
using web_api.Services.Authentication;

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
        public async Task<ActionResult<string>> SendTo([FromBody] UserDTO request)
        {
            await _notification.SendToAsync("Hallo Welt", "Test - Nachricht", request.Email);
            return Ok("test");
        }

    }
}

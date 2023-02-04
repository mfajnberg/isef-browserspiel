using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.DTOs;
using web_api.Services.Authentication;

namespace web_api.Controllers
{
    /// <summary>
    /// Test Notification Endpoint
    /// </summary>
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly INotification _notification;
        /// <summary>
        /// Constructor for <c>NotificationController</c> class
        /// </summary>
        /// <param name="configuration">application config data for SMTP credentials</param>
        /// <param name="notification">the notification which needs to be sent to the user</param>
        public NotificationController(IConfiguration configuration, INotification notification)
        {
            _configuration = configuration;
            _notification =   notification;
        }

        /// <summary>
        /// Sends a Notification to the given <c>User.Email</c>
        /// </summary>
        /// <response code="200">when the OGI has been successfully stored in the database</response>
        /// <param name="request"><c>UserDTO</c>, with Email </param>
        /// <returns><c>test</c></returns>
        [HttpPost("api/notification/sendto")]
        public async Task<ActionResult<string>> SendTo([FromBody] UserDTO request)
        {
            await _notification.SendToAsync("Hallo Welt", "Test - Nachricht", request.Email);
            return Ok("test");
        }

    }
}

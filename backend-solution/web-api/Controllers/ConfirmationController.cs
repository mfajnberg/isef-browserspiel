using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using web_api.Services.Authentication;

namespace web_api.Controllers
{
    public class ConfirmationController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly INotification _notification;

        public ConfirmationController(DataContext context, IConfiguration configuration, INotification notification)
        {
            _context = context;
            _configuration = configuration;
            _notification = notification;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="confirmationId"></param>
        /// <returns></returns>
        [HttpGet("api/confirmation/confirm/{confirmationId}")]
        public async Task<ActionResult<string>> Confirm([FromRoute] string confirmationId)
        {
            // check if the requested confimationId is known
            var confirm = _context.Confirmations.Where(c => c.ConfirmationId == confirmationId).FirstOrDefault();
            if (confirm == null)
                return NotFound();

            // check if the user of the requested confirmationId is known
            var user = _context.Users.Where(u => u.Id == confirm.UserId).FirstOrDefault();
            if (user == null)
                return BadRequest("User to confirm not Found");

            // check if the confirmation has already been processed
            if (user.IsActive)
                return BadRequest("User already confirmed");


            // set the active flag and proceed
            user.IsActive = true;
            await _context.SaveChangesAsync();

            return Ok("Vielen Dank, jetzt kann's richtig los gehen!");
        }
    }
}

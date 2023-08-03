using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using web_api.Services.Authentication;

namespace web_api.Controllers
{
    /// <summary>
    ///     Controller for email or text notification based activation of currently deactivated accounts
    /// </summary>
    [ApiController]
   public class ConfirmationController : Controller
    {
        private readonly DataContext _context;

        public ConfirmationController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Checks the confirmation Id from the request against the database and activates the user.
        /// </summary>
        /// 
        /// <param name="confirmationId">
        ///     The Id of the confirmation to be checked for.
        /// </param>
        /// 
        /// <response code="400">
        ///     The user account was already activated.
        /// </response>        
        /// 
        /// <response code="409">
        ///     The confirmation Id from the request was not found in the database.
        /// </response>
        /// 
        /// <response code="422">
        ///     The confirmation Id was found but the associated user Id is invalid.
        ///     This could happen if the account has been terminated after the activation was requested.
        /// </response>
        /// 
        /// <returns>
        ///     Message for the client.
        /// </returns>
        [HttpGet("api/confirmation/confirm/{confirmationId}")]
        public async Task<ActionResult<string>> Confirm([FromRoute] string confirmationId)
        {
            // check if the requested confimation Id is known
            var confirm = _context.Confirmations.Where(c => c.ConfirmationId == confirmationId).FirstOrDefault();
            if (confirm == null)
                return NotFound("Invalid confirmation ID.");

            // check if the user of the requested confirmation Id is known
            var user = _context.Users.Where(u => u.Id == confirm.UserId).FirstOrDefault();
            if (user == null)
                return UnprocessableEntity("Invalid user.");

            // check if the confirmation has already been processed
            if (user.IsActive)
                return BadRequest("User already active.");


            // set the active flag and proceed
            user.IsActive = true;
            await _context.SaveChangesAsync();

            string homepage = $"{Request.Scheme}://{Request.Host.Value}";
            return Ok($"Thank you, please follow '{homepage}' to beginn your journey!");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using web_api.Services.Authentication;

namespace web_api.Controllers
{
    /// <summary>
    /// Confirmation Endpoint
    /// </summary>
    [ApiController]
   public class ConfirmationController : Controller
    {
        private readonly DataContext _context;

        /// <summary>
        /// Constructor for <c>ConfirmationController</c>
        /// </summary>
        /// <param name="context"><c>DataContext</c> for Database interactions</param>
        public ConfirmationController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Confirms a new user
        /// </summary>
        /// <response code="200">when the new user has been successfully confirmed</response>
        /// <response code="400">when the user, who needs confirmtion was not found, or is already confirmed</response>
        /// <response code="409">when the confirmation-id is not found in the database</response>
        /// <param name="confirmationId">the confirmation id</param>
        /// <returns>message to the user</returns>
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

            string homepage = $"{Request.Scheme}://{Request.Host.Value}";
            return Ok($"Thank you, please follow '{homepage}' to beginn your journey!");
        }
    }
}

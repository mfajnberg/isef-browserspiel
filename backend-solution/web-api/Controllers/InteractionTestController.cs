using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api.DTOs;
using web_api.GameModel.OGIs;

namespace web_api.Controllers
{
    /// <summary>
    /// Endpoint for <c>OngoingGameplayInteraction</c>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionTestController : ControllerBase
    {
        DataContext _context;
        /// <summary>
        /// Constructor for <c>InteractionTestController</c>
        /// </summary>
        /// <param name="context"> TGype of <c>DataContext</c> for Database interactions</param>
        public InteractionTestController(DataContext context)
        {
            _context= context;  
        }

        /// <summary>
        /// Schedules a new <c>OngoingGemaplayInteraction</c> in the database
        /// </summary>
        /// <response code="200">when the OGI has been successfully stored in the database</response>
        /// <response code="400">if the OGI violates the rules</response>
        /// <param name="request"><c>UserDTO</c>, with Email and Password</param>
        /// <returns>the scheduled OGI</returns>
        [HttpPost]
        public async Task<ActionResult> ScheduleTestInteraction([FromBody] OGIDTO input)
        {
            TravelOGI interaction = new TravelOGI();
            interaction.ScheduledFor = input.ScheduledFor;
            interaction.ScheduledAt = DateTime.Now;

            if (interaction.ValidateRule(_context))
            {
                _context.TravelOGIs.Add(interaction);
                await _context.SaveChangesAsync();

                return Ok(interaction);
            }
            else
                return BadRequest();
        }
    }
}

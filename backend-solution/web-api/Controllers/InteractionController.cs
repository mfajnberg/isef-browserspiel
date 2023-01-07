using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api.GameModel;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionController : ControllerBase
    {
        DataContext _context;
        public InteractionController(DataContext context)
        {
            _context= context;  
        }
        [HttpPost]   
        public async Task<ActionResult> ScheduleInteraction([FromBody]OngoingInteractionDto input)
        {
            OngoingInteraction interaction = new OngoingInteraction();
            interaction.ScheduledFor = input.ScheduledFor;
            interaction.ScheduledAt = DateTime.Now;

            if (interaction.ValidateRule(_context))
            {
                _context.Interactions.Add(interaction);
                await _context.SaveChangesAsync();

                return Ok(interaction);
            }
            else
                return BadRequest();
        }
    }
}

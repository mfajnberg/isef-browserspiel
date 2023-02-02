﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api.DTOs;
using web_api.GameModel.OGIs;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionTestController : ControllerBase
    {
        DataContext _context;
        public InteractionTestController(DataContext context)
        {
            _context= context;  
        }
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

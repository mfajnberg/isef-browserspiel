using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net.Http.Headers;
using System.Security.Claims;
using web_api.DTOs;
using web_api.GameModel;
using web_api.GameModel.Creatures;
using web_api.GameModel.Worldmap;

namespace web_api.Controllers
{
    [Authorize]
    [Route("api/party")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        DataContext _context;
        public PartyController(DataContext context)
        {
            _context= context;
        }
        [HttpGet("vision")]
        public async Task<ActionResult> GetVisibleHexTiles()
        {
            var user = GetUserFromClaim();
            if (user == null)
                return BadRequest();

            Avatar avatar = user.Avatar;
            HexVector dtoVec = new HexVector(avatar.Fellowship.Location.AxialQ, avatar.Fellowship.Location.AxialR);
            HexTileDTO dto = new HexTileDTO() { AxialCoordinates = dtoVec };

            List<HexTile> result = await WorldManager.GetSliceAsync(_context, dto);

            return Ok(result);
        }

        [HttpPost("travel")]
        public async Task<ActionResult> TravelTo([FromBody] HexTileDTO destination )
        {
            var user = GetUserFromClaim();
            if (user?.Avatar?.Fellowship == null)
                return BadRequest();

            var destinationHex = _context.HexTiles.Where(h => h.AxialR == destination.AxialCoordinates.R 
                                            && h.AxialQ == destination.AxialCoordinates.Q).FirstOrDefault();

            if (destinationHex == null)
                return BadRequest("Destination not found");

            await user.Avatar.Fellowship.StartTravelingAsync(destinationHex, _context);

            //OngoingInteraction interaction = new OngoingInteraction();
            //interaction.ScheduledAt = DateTime.Now;
            //interaction.ScheduledFor = DateTime.Now.AddSeconds(7);
            //_context.Interactions.Add(interaction);
            //await _context.SaveChangesAsync();

            return Ok();


        }


        private User? GetUserFromClaim()
        {
            var mailFromClaim = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value.ToLower();
            var user = _context.Users.Where(u => u.Email.ToLower() == mailFromClaim);
            return (User)user;
        }
    } 
}

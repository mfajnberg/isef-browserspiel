using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net.Http.Headers;
using System.Security.Claims;
using web_api.DTOs;
using web_api.GameModel;
using web_api.GameModel.Creatures;
using web_api.GameModel.OGIs;
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

        [HttpGet("get")]
        public async Task<ActionResult> GetParty()
        {
            var user = GetUserFromClaim();
            var response = user == null ? UnprocessableEntity("User")
                : user?.Avatar == null ? UnprocessableEntity("Avatar")
                : user?.Avatar?.Party == null ? UnprocessableEntity("Party")
                : null;
            if (response != null)
                return response;

            return Ok(user.Avatar.Party);
        }

        [HttpGet("vision")]
        public async Task<ActionResult> GetVisibleHexTiles()
        {
            var user = GetUserFromClaim();
            if (user == null)
                return BadRequest();

            Avatar avatar = user.Avatar;
            HexVector dtoVec = new HexVector(avatar.Party.Location.AxialQ, avatar.Party.Location.AxialR);
            HexTileDTO dto = new HexTileDTO() { AxialCoordinates = dtoVec };

            List<HexTile> result = await WorldManager.GetSliceAsync(_context, dto);

            return Ok(result);
        }

        [HttpPost("travel")]
        public async Task<ActionResult> TravelTo([FromBody] HexTileDTO destination )
        {
            var user = GetUserFromClaim();
            var response = user == null ? UnprocessableEntity("User")
                : user?.Avatar == null ? UnprocessableEntity("Avatar")
                : user?.Avatar?.Party == null ? UnprocessableEntity("Party")
                : null;
            if (response != null) 
                return response;

            var destinationHex = _context.HexTiles.Where(h => h.AxialR == destination.AxialCoordinates.R 
                                            && h.AxialQ == destination.AxialCoordinates.Q).FirstOrDefault();
            if (destinationHex == null)
                return UnprocessableEntity("Destination");

            await user.Avatar.Party.StartTravelingAsync(destinationHex, _context);

            TravelOGI travel = new TravelOGI();
            travel.ScheduledAt = DateTime.Now;
            travel.ScheduledFor = DateTime.Now.AddSeconds(7); // add rules to calculate travel time
            _context.TravelOGIs.Add(travel);
            await _context.SaveChangesAsync();

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

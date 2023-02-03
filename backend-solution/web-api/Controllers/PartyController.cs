using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
                : user?.Avatar?.PartyId == null ? UnprocessableEntity("Party")
                : null;
            if (response != null)
                return response;

            var party = _context.Parties.Where(p => p.Id == user.Avatar.PartyId).FirstOrDefault();

            return Ok(party);
        }

        [HttpGet("vision")]
        public async Task<ActionResult> GetVisibleHexTiles()
        {
            var user = GetUserFromClaim();
            var response = user == null ? UnprocessableEntity("User")
                : user?.Avatar == null ? UnprocessableEntity("Avatar")
                : null;
            if (response != null)
                return response;

            Party party = _context.Parties.Include(p => p.Location).Where(p => p.Id == user.Avatar.PartyId).FirstOrDefault();

            if (party == null)
            {
                return UnprocessableEntity("Party");
            }

            List<HexTile> result = await WorldManager.GetSliceAsync(_context, 
                new HexTileDTO() { 
                    AxialCoordinates = new HexVector(party.Location.AxialQ, party.Location.AxialR) 
                });

            return Ok(result);
        }

        [HttpPost("travel")]
        public async Task<ActionResult> TravelTo([FromBody] HexTileDTO destination )
        {
            var user = GetUserFromClaim();
            var response = user == null ? UnprocessableEntity("User")
                : user?.Avatar == null ? UnprocessableEntity("Avatar")
                : null;
            if (response != null) 
                return response;

            var destinationHex = _context.HexTiles.Where(h => h.AxialR == destination.AxialCoordinates.R 
                                            && h.AxialQ == destination.AxialCoordinates.Q).FirstOrDefault();
            if (destinationHex == null)
                return UnprocessableEntity("Destination");

            var party = _context.Parties.Where(p => p.Id == user.Avatar.PartyId).FirstOrDefault();

            await party.StartTravelingAsync(destinationHex, _context);

            TravelOGI travel = new TravelOGI();
            travel.ScheduledAt = DateTime.Now;
            travel.ScheduledFor = DateTime.Now.AddSeconds(7); // add rules to calculate travel time
            _context.TravelOGIs.Add(travel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // also write a "GetPartyFromUser" or maybe "GetPartyFromClaim" method?
        private User? GetUserFromClaim()
        {
            var mailFromClaim = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value.ToLower();
            var user = _context.Users.Include(u => u.Avatar).Where(u => u.Email.ToLower() == mailFromClaim).FirstOrDefault();

            return user;
        }
    } 
}

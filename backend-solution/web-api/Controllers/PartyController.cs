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
using web_api.GameModel.Items;
using web_api.GameModel.OGIs;
using web_api.GameModel.Worldmap;

namespace web_api.Controllers
{
    /// <summary>
    ///     Controller for issuing Party-related gameplay interactions
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/party")]
    public class PartyController : ControllerBase
    {
        DataContext _context;
        public PartyController(DataContext context)
        {
            _context= context;
        }

        /// <summary>
        ///     Retrieves the requesting client's avatar's associated party
        /// </summary>
        /// 
        /// <response code="422">
        ///     Some entity within the chain of references on a processed entity is invalid
        /// </response>
        /// 
        /// <returns> 
        ///     The user's <c>Party</c>-object requested by the client
        /// </returns>
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

        /// <summary>
        ///     Returns visible hex tile data from the vision radius of the requesting user's associated party
        /// </summary>
        /// 
        /// <response code="422">
        ///     Some entity within the chain of references on a processed entity is invalid
        /// </response>
        /// 
        /// <returns> 
        ///     List of <c>HexTiles</c> to be displayed to the player
        /// </returns>
        [HttpPost("vision")]
        public async Task<ActionResult> GetVisibleHexTiles([FromBody] HexTileDTO lookingFrom) // take "lookingFrom" hex tile dto argument
        {
            var user = GetUserFromClaim();
            var response = user == null ? UnprocessableEntity("User")
                : user?.Avatar == null ? UnprocessableEntity("Avatar")
                : null;
            if (response != null)
                return response;

            // do this only for validation purposes
            Party party = _context.Parties.Include(p => p.Location).Where(p => p.Id == user.Avatar.PartyId).FirstOrDefault();

            if (party == null)
            {
                return UnprocessableEntity("Party");
            }

            List<HexTile> result = await WorldManager.GetSliceAsync(_context, lookingFrom);

            return Ok(result);
        }

        /// <summary>
        ///     Mutates the current destination that the user's party is traveling to.
        ///     
        /// </summary>
        /// 
        /// <param name="travelPath">
        ///     the destionation of the current user travel
        /// </param>
        ///
        /// <response code="422">
        ///     Some entity within the chain of references on a processed entity is invalid
        /// </response>
        [HttpPost("travel")]
        public async Task<ActionResult> TravelTo([FromBody] List<HexTileDTO> travelPath)
        {
            var user = GetUserFromClaim();
            var response = user == null ? UnprocessableEntity("User")
                : user?.Avatar == null ? UnprocessableEntity("Avatar")
                : null;
            if (response != null) 
                return response;

            List<HexTile> hexTilesPath = null;
            try
            {
                hexTilesPath = WorldManager.ValidatePath(_context, travelPath);
            }
            catch (Exception ex)
            {
                return BadRequest($"Path validation failed: {ex.Message}");
            }

            if (hexTilesPath != null)
            {
                var party = _context.Parties.Where(p => p.Id == user.Avatar.PartyId).FirstOrDefault();
                if (party == null)
                    UnprocessableEntity("Party");

                DateTime scheduleTimeAt = DateTime.Now;
                DateTime scheduleTimeFor = DateTime.Now;
                
                List<TravelOGI> newTravelOgis = new List<TravelOGI>();
                for (int i = 0; i < hexTilesPath.Count; i++)
                {
                    TravelOGI travel = new TravelOGI();
                    travel.Id = 0;
                    travel.PartyId = party.Id;
                    travel.TargetAxialQ = hexTilesPath[i].AxialQ;
                    travel.TargetAxialR = hexTilesPath[i].AxialR;
                    travel.ScheduledAt = scheduleTimeAt;

                    //scheduleTimeFor = scheduleTimeFor.AddSeconds(hexTilesPath[i].TravelTime);
                    scheduleTimeFor = scheduleTimeFor.AddSeconds(2);
                    travel.ScheduledFor = scheduleTimeFor; // add rules to calculate travel time

                    newTravelOgis.Add(travel);
                }

                 _context.TravelOGIs.AddRange(newTravelOgis);
                 _context.SaveChanges();

                await party.StartTravelingAsync(hexTilesPath[hexTilesPath.Count -1], _context);
            }
            return Ok();
        }

        /// <summary>
        ///     Retrieves a user with their avatar from the database based on the email claim of the authenticated user
        /// </summary>
        /// 
        /// <returns> 
        ///     The user object or null if not found 
        /// </returns>
        private User? GetUserFromClaim()
        {
            var mailFromClaim = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value.ToLower();
            var user = _context.Users.Include(u => u.Avatar).Where(u => u.Email.ToLower() == mailFromClaim).FirstOrDefault();

            return user;
        }
    } 
}

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
    /// Party Endpoint
    /// </summary>
    [Authorize]
    [Route("api/party")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        DataContext _context;
        /// <summary>
        /// Constructor for PartyController
        /// </summary>
        /// <param name="context">type of <c>DataContext</c> for Database interactions</param>
        public PartyController(DataContext context)
        {
            _context= context;
        }

        /// <summary>
        /// Gets the party from the current user
        /// </summary>
        /// <response code="200">when the party was found</response>
        /// <response code="422">if any error occured while getting the users party</response>
        /// <returns> the <c>Party</c>-object from the current user</returns>
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
        /// Gets the vision of the party from the current user
        /// </summary>
        /// <response code="200">when the vision of party was found</response>
        /// <response code="422">if any error occured while getting the current vision ot the users party</response>
        /// <returns> List of <c>HexTiles</c> which should be displayed to the user</returns>
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
        /// Sets an new destination the user wants to travel to
        /// </summary>
        /// <response code="200">when the <c>TravelOGI</c> has been successfully scheduled</response>
        /// <response code="422">if any error occured while getting <c>Avatar</c>, <c>HexTile</c></response>
        /// <param name="path">the destionation of the current user travel</param>
        [HttpPost("travel")]
        public async Task<ActionResult> TravelTo([FromBody] List<HexTileDTO> path )
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
                hexTilesPath = WorldManager.ValidatePath(_context, path);
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
                    travel.AxialQ = hexTilesPath[i].AxialQ;
                    travel.AxialR = hexTilesPath[i].AxialR;
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

        // also write a "GetPartyFromUser" or maybe "GetPartyFromClaim" method?
        private User? GetUserFromClaim()
        {
            var mailFromClaim = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value.ToLower();
            var user = _context.Users.Include(u => u.Avatar).Where(u => u.Email.ToLower() == mailFromClaim).FirstOrDefault();

            return user;
        }
    } 
}

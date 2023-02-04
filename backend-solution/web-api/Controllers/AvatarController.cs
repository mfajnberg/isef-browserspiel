using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;
using web_api.DTOs;
using web_api.GameModel;
using web_api.GameModel.Creatures;
using web_api.Services;

namespace web_api.Controllers
{
    /// <summary>
    /// Avatar Endpoint
    /// </summary>
    [Authorize]
    [Route("api/avatar")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly DataContext _context;
        /// <summary>
        /// Constructor for AvatarController
        /// </summary>
        /// <param name="context"></param>
        public AvatarController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of premade characters
        /// </summary>
        /// <returns></returns>
        /// <response code="200">returns a list of premade avatars</response>
         [HttpGet("choices")]
        public async Task<ActionResult> GetPremadeChoices()
        {
            return Ok(PremadeAvatars.GetAvatarList());
        }

        /// <summary>
        /// sets the given premade character to the current user
        /// </summary>
        /// <param name="name">name of the premade character</param>
        /// <returns></returns>
        ///  <response code="200">when the avatar was set successfully to the user</response>
        /// <response code="400">if the selected avatar was not found</response>
        /// <remarks>
        /// Sample request:
        ///  POST
        ///  {
        ///    "Leito Froste"
        ///  }
        /// </remarks>
        [HttpPost("select")]
        public async Task<ActionResult> SelectAvatar([FromBody] string name)
        {
            var mailFromClaim = User.Claims
                .Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value.ToLower();
            var user = _context.Users.Where(u => u.Email.ToLower() == mailFromClaim).FirstOrDefault();
            if (user != null)
            {
                if (user.Avatar == null)
                {
                    Avatar newAvatar;
                    try
                    {
                        // replace with proper avatar generation...
                        newAvatar = PremadeAvatars.SelectFromAvatarList(name);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    user.Avatar = newAvatar;
                    _context.Avatars.Add(newAvatar);
                    _context.SaveChanges();

                    Party party = await createNewPartyAsync(user.Avatar);

                    AuthResponseDTO responseDTO = new AuthResponseDTO();
                    responseDTO.Avatar = newAvatar;
                    responseDTO.Party = party;
                    responseDTO.IsAdmin = user.IsAdmin;

                    return Ok(responseDTO);
                }
                return BadRequest("You already have an avatar");
            }
            return BadRequest("User not found");
        }

        /// <summary>
        /// every avatar needs a party, 
        /// </summary>
        /// <param name="avatar">the newly created avatar</param>
        /// <returns>the new <c>Party</c>-object</returns>
        private async Task<Party> createNewPartyAsync(Avatar? avatar)
        {
            Party party = new Party();
            party.LeaderId = avatar.Id;
            
            _context.Parties.Add(party);
            await _context.SaveChangesAsync();

            avatar.PartyId = party.Id;
            await _context.SaveChangesAsync();

            return party;

        }
    }
}

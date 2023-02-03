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
    [Authorize]
    [Route("api/avatar")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AvatarController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Gets a list of premade characters
        /// </summary>
        /// <response code="200"></response>
        /// <returns></returns>
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

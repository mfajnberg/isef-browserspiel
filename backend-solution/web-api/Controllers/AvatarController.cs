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
    ///     Controller for issuing Avatar-related gameplay interactions
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/avatar")]
    public class AvatarController : ControllerBase
    {
        private readonly DataContext _context;

        public AvatarController(DataContext context)
        {
            _context = context;
        }

        // todo: Replace premade avatar selection with a properly designed character creation process!
        /// <summary>
        ///     Presents the player with a selection of three premade avatar choices.
        /// </summary>
        /// 
        /// <returns>
        ///     A list of premade characters
        /// </returns>
        [HttpGet("choices")]
        public async Task<ActionResult> GetPremadeChoices()
        {
            return Ok(PremadeAvatars.GetAvatarList());
        }

        /// <summary>
        ///     sets the given premade character to the current user
        /// </summary>
        /// 
        /// <param name="name">
        ///     name of the premade character
        /// </param>
        /// 
        /// <response code="400">
        ///     if the selected avatar was not found
        /// </response>
        /// 
        /// <remarks>
        ///     Sample request:
        /// 
        ///     POST
        ///     
        ///     {
        ///         "Leito Froste"
        ///     }
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
                        newAvatar.Id = 0;
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
        ///     Every avatar needs a party, 
        /// </summary>
        /// 
        /// <param name="avatar">
        ///     The newly created avatar
        /// </param>
        /// 
        /// <returns>
        ///     The new <c>Party</c>-object
        /// </returns>
        private async Task<Party> createNewPartyAsync(Avatar? avatar)
        {
            Party party = new Party();
            party.LeaderId = avatar.Id;
            party.Electrum = 100;
            party.Location = _context.HexTiles.Where(h => h.AxialQ == 0 && h.AxialR == 0).FirstOrDefault();
            
            _context.Parties.Add(party);
            await _context.SaveChangesAsync();

            avatar.PartyId = party.Id;
            await _context.SaveChangesAsync();

            return party;

        }
    }
}

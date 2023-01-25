using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using web_api.GameModel.Creatures;
using web_api.Services;

namespace web_api.Controllers
{
    [ApiController]
    public class AvatarCreationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AvatarCreationController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Gets a list of premade characters
        /// </summary>
        /// <response code="200"></response>
        /// <returns></returns>
        [HttpGet("api/create-avatar/get-premade-choices")]
        public async Task<ActionResult> GetPremadeChoices()
        {
            return Ok(PremadeAvatars.GetAvatarList());
        }

        /// <summary>
        /// sets the given premade character to the current user
        /// </summary>
        /// <param name="name">name of the premade character</param>
        /// <returns></returns>
        // [Authorize]
        [HttpGet("api/create-avatar/choose-premade")]
        public async Task<ActionResult> ChoosePremade(string? name)
        {
            Creature avatarChoice;
            try
            {
                avatarChoice = PremadeAvatars.SelectFromAvatarList(name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // todo: get user from db based on [token, email, cookie]
            // set avatar for user
            // save changes
            // return Ok

            return Ok();
        }


        [HttpPost("api/create-avatar")]
        [Authorize]
        public async Task<ActionResult> SetAvatar([FromBody] AvatarDto avatar)
        {
            var mailFromClaim = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value.ToLower();
            var user = _context.Users.Where(u => u.Email.ToLower() == mailFromClaim).FirstOrDefault();
            if (user != null)
            {
                if (user.Avatar == null)
                {
                    Avatar newAvatar = new Avatar { Name = avatar.Name };
                    newAvatar.Fellowship = new GameModel.Party();
                    user.Avatar = newAvatar;
                    _context.Avatars.Add(newAvatar);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            return BadRequest("You already have an avatar");
        }
    }
}

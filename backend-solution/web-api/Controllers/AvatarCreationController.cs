using Microsoft.AspNetCore.Mvc;
using web_api.GameModel.AvatarModel;
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
            CreatureBase avatarChoice;
            try
            {
                avatarChoice = PremadeAvatars.SelectFromAvatarList(name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

<<<<<<< Updated upstream
            // todo: get user from db based on [token, email, cookie]
            // set avatar for user
=======
            // track user from based on token/email/cookie
            // put selected avatar for user
>>>>>>> Stashed changes
            // save changes
            // return Ok

            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("api/create-avatar/get-premade-choices")]
        public async Task<ActionResult> GetPremadeChoices()
        {
            return Ok(PremadeAvatars.GetAvatarList());
        }

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

            // get user from db based on [token, email, cookie]
            // set avatar for user
            // save changes
            // return Ok

            return Ok();
        }
    }
}
